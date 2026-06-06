using Application.DTOs.AdminArea;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
	public class AdminAreaService
	{
		private readonly IAdminAreaRepository _adminAreaRepository;

		public AdminAreaService(IAdminAreaRepository adminAreaRepository)
		{
			_adminAreaRepository = adminAreaRepository;
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

		public async Task<AdminAreaDTO> CreateAdminArea(CreateAdminAreaDTO dto)
		{
			var adminArea = new AdminArea
			{
				Id = dto.UserId,
				BranchId = dto.BranchId,
				IsActive = true,
				CreatedAt = DateTime.Now
			};

			var result = await _adminAreaRepository.Create(adminArea);

			return new AdminAreaDTO
			{
				UserId = result.Id,
				BranchId = result.BranchId,
				IsActive = result.IsActive,
				CreatedAt = result.CreatedAt
			};
		}

		public async Task<int> UpdateAdminArea(int id, UpdateAdminAreaDTO dto)
		{
			var adminArea = await _adminAreaRepository.GetById(id);

			if (adminArea == null)
			{
				return -1;
			}

			adminArea.Id = dto.UserId;
			adminArea.BranchId = dto.BranchId;
			adminArea.IsActive = dto.IsActive;

			await _adminAreaRepository.Update(adminArea);

			return adminArea.Id;
		}

		public async Task<bool> DeleteAdminArea(int id)
		{
			return await _adminAreaRepository.Delete(id);
		}
	}
}