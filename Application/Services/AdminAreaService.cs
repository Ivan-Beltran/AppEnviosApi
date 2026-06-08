using Application.DTOs.AdminArea;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
	public class AdminAreaService
	{
		private readonly IAdminAreaRepository _adminAreaRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;
        private readonly ITokenService _tokenService;

        public AdminAreaService(IAdminAreaRepository adminAreaRepository, IUserRepository userRepository, IPasswordService passwordService, ITokenService tokenService)
		{
			_adminAreaRepository = adminAreaRepository;
			_userRepository = userRepository;
			_passwordService = passwordService;
			_tokenService = tokenService;
        }

		public async Task<List<AdminAreaDTO>> GetAllAdminArea()
		{
			var adminAreas = await _adminAreaRepository.GetAll();

			return adminAreas.Select(a => new AdminAreaDTO
			{
				UserId = a.Id,

				FullName = a.User != null ? a.User.FullName : null,

				BranchId = a.BranchId,
				BranchName = a.Branch != null ? a.Branch.BranchName : null,

				IsActive = a.IsActive,
				CreatedAt = a.CreatedAt
			}).ToList();
		}

		public async Task<AdminAreaDTO?> GetByIdAdminArea(int id)
		{
			var adminArea = await _adminAreaRepository.GetById(id);

			if (adminArea == null)
			{
				return null;
			}

			return new AdminAreaDTO
			{
				UserId = adminArea.Id,
				
				FullName = adminArea.User != null ? adminArea.User.FullName : null,

				BranchId = adminArea.BranchId,
				BranchName = adminArea.Branch != null ? adminArea.Branch.BranchName : null,

				IsActive = adminArea.IsActive,
				CreatedAt = adminArea.CreatedAt
			};
		}

		public async Task<int> CreateAdminArea(CreateAdminAreaDTO dto)
		{
            var (passwordHash, salt) = _passwordService.HashPassword(dto.Password);
            User userBase = new User()
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email,
                Password = passwordHash,
                Salt = salt,
                RoleId = 2,
                IsActive = true
            };

            var createdUser = await _userRepository.Create(userBase);
            
            AdminArea adminArea = new AdminArea()
            {
				Id = createdUser.Id,
                BranchId = dto.BranchId,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _adminAreaRepository.Create(adminArea);

			return createdUser.Id;
		}

        public async Task<int> UpdateAdminArea(int id, UpdateAdminAreaDTO dto)
        {
            using var transaction = await _userRepository.BeginTransactionAsync();

            try
            {
                // Buscar usuario base
                var user = await _userRepository.GetById(id);
                if (user == null || user.RoleId != 2) // 2 = AdminArea
                    return -1;

                // Actualizar datos de User
                user.FullName = dto.FullName;
                user.Phone=dto.Phone;
                user.Email = dto.Email;

                await _userRepository.Update(user);

                // Buscar registro de AdminArea
                var adminArea = await _adminAreaRepository.GetById(id);
                if (adminArea == null)
                    return -1;

                // Actualizar datos específicos de AdminArea
                adminArea.BranchId = dto.BranchId;
              

                await _adminAreaRepository.Update(adminArea);

                await transaction.CommitAsync();

                return user.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeleteAdminArea(int id)
		{
			return await _adminAreaRepository.Delete(id);
		}
	}
}