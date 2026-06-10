using Application.DTOs.CompanyClient;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class CompanyClientService
    {
        private readonly ICompanyClient _companyClient;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public CompanyClientService(ICompanyClient companyClient, IUserRepository userRepository, IPasswordService passwordService)
        {
            _companyClient = companyClient;
            _userRepository = userRepository;
            _passwordService = passwordService;
        }

        public async Task<List<CompanyClientDTO>> GetAllCompanyClient()
        {
            var companyClient = await _companyClient.GetAll();

            return companyClient.Select(a => new CompanyClientDTO
            {
                Id = a.Id,
                FullName = a.User.FullName,
                Email=a.User.Email,
                UserPhone=a.User.Phone,
                CompanyName=a.CompanyName,
                CompanyPhone=a.CompanyPhone,
                CompanyAddress=a.CompanyAddress,
                DistricId = a.DistrictId,
                DistricName = a.District.DistrictName,
                IsActive = a.IsActive,
                CreatedAt = a.CreatedAt
            }).ToList();
        }

        public async Task<CompanyClientDTO?> GetByIdCompanyClient(int id)
        {
            var companyClient = await _companyClient.GetById(id);

            if (companyClient == null)
            {
                return null;
            }

            return new CompanyClientDTO
            {
                Id= companyClient.Id,
                FullName = companyClient.User.FullName,
                Email = companyClient.User.Email,
                UserPhone = companyClient.User.Phone,
                CompanyName = companyClient.CompanyName,
                CompanyPhone = companyClient.CompanyPhone,
                CompanyAddress = companyClient.CompanyAddress,
                DistricId = companyClient.DistrictId,
                DistricName = companyClient.District.DistrictName,
                IsActive = companyClient.IsActive,
                CreatedAt = companyClient.CreatedAt
            };
        }

        public async Task<int> CreateCompanyClient(CreateCompanyClientDTO dto)
        {
            var (passwordHash, salt) = _passwordService.HashPassword(dto.Password);
            User userBase = new User()
            {
                FullName = dto.FullName,
                Phone = dto.UserPhone,
                Email = dto.Email,
                Password = passwordHash,
                Salt = salt,
                RoleId = 4,
                IsActive = true
            };

            var createdUser = await _userRepository.Create(userBase);

            CompanyClient companyClient = new CompanyClient()
            {
                Id= createdUser.Id,
                CompanyName = dto.CompanyName,
                CompanyPhone = dto.CompanyPhone,
                CompanyAddress = dto.CompanyAddress,
                DistrictId = dto.DistricId,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _companyClient.Create(companyClient);

            return createdUser.Id;
        }

        public async Task<int> UpdateCompanyClient(int id, UpdateCompanyClientDTO dto)
        {
            using var transaction = await _userRepository.BeginTransactionAsync();

            try
            {
                // Buscar usuario base
                var user = await _userRepository.GetById(id);
                if (user == null || user.RoleId != 4) // 2 = cliente
                    return -1;

                // Actualizar datos de User
                user.FullName = dto.FullName;
                user.Phone = dto.UserPhone;
                user.Email = dto.Email;

                await _userRepository.Update(user);

                // Buscar registro de AdminArea
                var companyClient = await _companyClient.GetById(id);
                if (companyClient == null)
                    return -1;

                // Actualizar datos específicos de AdminArea
                companyClient.DistrictId = dto.DistricId;


                await _companyClient.Update(companyClient);

                await transaction.CommitAsync();

                return user.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteCompanyClient(int id)
        {
            return await _companyClient.Delete(id);
        }
    }
}
