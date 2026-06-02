using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class BranchService
    {
		private readonly IBranchRepository _branchRepository;

		public BranchService(IBranchRepository branchRepository)
		{
			_branchRepository = branchRepository;
		}

		public async Task<List<BranchDTO>> ObtenerTodos()
		{
			var branches = await _branchRepository.ObtenerTodos();

			return branches.Select(branch => new BranchDTO
			{
				Id = branch.Id,
				BranchName = branch.BranchName,
				Address = branch.Address,
				DistrictId = branch.DistrictId,
				DistrictName = branch.District?.DistrictName ?? "sin distrito",
                DepartmentName= branch.District?.Department?.DepartmentName ?? "sin departamento"

            }).ToList();
		}

		public async Task<BranchDTO?> ObtenerPorId(int id)
		{
			var branch = await _branchRepository.ObtenerPorId(id);

			if (branch == null)
			{
				return null;
			}

			return new BranchDTO
			{
				Id = branch.Id,
				BranchName = branch.BranchName,
				Address = branch.Address,
				DistrictId = branch.DistrictId
			};
		}

		public async Task<BranchDTO> Crear(BranchDTO  branchDTO)
		{
			var branch = new Branch
			{
				BranchName = branchDTO.BranchName,
				Address = branchDTO.Address,
				DistrictId = branchDTO.DistrictId
			};

			var branchCreada = await _branchRepository.Crear(branch);

			branchDTO.Id = branchCreada.Id;

			return branchDTO;

		}
		public async Task<BranchDTO?> Actualizar(int id, BranchDTO branchDTO)
		{
			var branch = new Branch
			{
				BranchName = branchDTO.BranchName,
				Address = branchDTO.Address,
				DistrictId = branchDTO.DistrictId
			};

			var branchActualizada = await _branchRepository.Actualizar(id, branch);

			if (branchActualizada == null)
			{
				return null;
			}

			return new BranchDTO
			{
				Id = branchActualizada.Id,
				BranchName = branchActualizada.BranchName,
				Address = branchActualizada.Address,
				DistrictId = branchActualizada.DistrictId
			};

		}

		public async Task<bool> Eliminar(int id)
		{
			return await _branchRepository.Eliminar(id);
		}

	}
}
