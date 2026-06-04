using Domain.Interfaces;
using Domain.Entities;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces;
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
