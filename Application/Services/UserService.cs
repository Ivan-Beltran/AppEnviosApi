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

        public UserService(IUserRepository userRepository, IPasswordService passwordService,IAdminRepository adminRepository)
        {
            _userRepository = userRepository;
            _passwordService = passwordService;
            _adminRepository = adminRepository;
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
    }
}
