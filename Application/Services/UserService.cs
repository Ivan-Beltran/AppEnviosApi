using Domain.Interfaces;
using Domain.Entities;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
using Application.DTOs.Admin;
namespace Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly IAdminRepository _adminRepository;
        private readonly ITokenService _tokenService;
    
            

        public UserService(IUserRepository userRepository, IPasswordService passwordService,IAdminRepository adminRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _adminRepository = adminRepository;
            _tokenService = tokenService;
        }
        
        public async Task<List<AdminGlobalDTO>> GetAllAdminGlobal()
        {
            var admins = await _adminRepository.GetAll();

            // 2. Mapeamos la lista de entidades hacia la estructura de DTOs anidados
            return admins.Select(admin => new AdminGlobalDTO
            {
                CreatedAt = admin.CreatedAt,
                IsActive = admin.IsActive,

                // Creamos el DTO de usuario recortado aquí mismo
                User = new UserDto
                {
                    FullName = admin.User.FullName,
                    Email = admin.User.Email,
                    Phone = admin.User.Phone,
                    IsActive=admin.User.IsActive
                   
                }
            }).ToList();
        }

        public async Task<AdminGlobalDTO?> GetByIdAdminGlobal(int id)
        {
            // 1. Buscamos el admin en el repositorio (que ya incluye al User)
            var admin = await _adminRepository.GetById(id);

            // 2. Si no existe, retornamos null (el controlador se encargará de mandar un 404)
            if (admin == null) return null;

            // 3. Mapeamos el objeto único a nuestro DTO con los datos recortados
            return new AdminGlobalDTO
            {
                CreatedAt = admin.CreatedAt,
                IsActive = admin.IsActive,

                // Mapeamos el sub-DTO de usuario con los campos públicos
                User = new UserDto
                {
                    FullName = admin.User.FullName,
                    Email = admin.User.Email,
                    Phone = admin.User.Phone
                }
            };
        }

        public async Task<bool> DeleteAdminGlobal(int id)
        {
            // 1. Validar primero si el administrador existe en el sistema
            var admin = await _adminRepository.GetById(id);
            if (admin == null)
            {
                throw new KeyNotFoundException($"El Administrador Global con ID {id} no existe.");
            }

            // 2. Si existe, procedemos a borrarlo (lógico o físico según tu repo)
            return await _adminRepository.Delete(id);
        }

        public async Task<int> CreateAdminGlobal(CreateUserDTO createUserdto)
        {
            var (passwordHash, salt) = _passwordService.HashPassword(createUserdto.Password);
            User userBase = new User()
            {
                FullName = createUserdto.FullName,
                Phone = createUserdto.Phone,
                Email = createUserdto.Email,
                Password = passwordHash,
                Salt = salt,
                RoleId = 1,
                IsActive=true
            };

            var createdUser= await _userRepository.Create(userBase);

            Admin AdminExtension = new Admin()
            {
                Id = createdUser.Id,
                IsActive= true,
                CreatedAt = DateTime.UtcNow
            };

            await _adminRepository.Create(AdminExtension);

            return createdUser.Id;
        }

        public async Task<int> UpdateAdminGlobal(int id, UpdateUserDTO updateUserDto)
        {
    // 1. Iniciar una transacción para asegurar consistencia entre ambas tablas
            using var transaction = await _userRepository.BeginTransactionAsync();

                try
                    {
        // 2. Buscar al usuario base en la tabla Users
                        var user = await _userRepository.GetById(id);
                        if (user == null || user.RoleId != 1) return -1; // RoleId 1 = Admin Global

        // 3. Actualizar los datos del Usuario Base
                        user.FullName = updateUserDto.FullName;
                        user.Phone = updateUserDto.Phone;
                        user.Email = updateUserDto.Email;
        
        // Asumiendo que agregaste IsActive al CreateUserDTO para poder editarlo
                        user.IsActive = updateUserDto.IsActive; 

        // Guardar cambios en la tabla User
                        await _userRepository.Update(user);

        // 4. Buscar y actualizar la tabla de extensión Admin
                        var adminExtension = await _adminRepository.GetById(id);
                        if (adminExtension == null)
                        {
                            return -1; // O lanzar una excepción si prefieres
                        }

        // Sincronizamos el estado IsActive en la tabla Admin también
                        adminExtension.IsActive = updateUserDto.IsActive;
        
        // Guardar cambios en la tabla Admin
                        await _adminRepository.Update(adminExtension);

        // 5. Si ambas operaciones tuvieron éxito, consolidamos los cambios en la BD
                        await transaction.CommitAsync();

                        return user.Id;
                    }
            catch (Exception)
            {
        // Si algo falla en el proceso, deshacemos todo para evitar datos corruptos
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<string?> Login(LoginDTO dto)
        {
           
            var user = await _userRepository.GetByEmailWithRole(dto.Email);

            if (user == null || !user.IsActive) return null;

            bool isPasswordCorrect = _passwordService.VerifyPassword(dto.Password, user.Salt, user.Password);

            if (!isPasswordCorrect)
            {
                return null; 
            }

            return _tokenService.GenerateToken(user);
            
        }

    }
}
