using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class BranchRepository : IBranchRepository
	{
		private readonly AppDbContext _context;

		public BranchRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<List<Branch>> GetAll()
		{
			return await _context.Branches
				.Include(b=> b.District)
					.ThenInclude(d => d.Department)
                .ToListAsync();
		}

		public async Task<Branch?> GetById(int id)
		{
			return await _context.Branches.FindAsync(id);
		}

		public async Task<Branch> Create(Branch branch)
		{
			_context.Branches.Add(branch);

			await _context.SaveChangesAsync();

			return branch;
		}

		public async Task<Branch?> Update(int id, Branch branch)
		{
			var branchExistente = await _context.Branches.FindAsync(id);

			if (branchExistente == null)
			{
				return null;
			}

			branchExistente.BranchName = branch.BranchName;
			branchExistente.Address = branch.Address;
			branchExistente.DistrictId = branch.DistrictId;

			await _context.SaveChangesAsync();

			return branchExistente;
		}

		public async Task<bool> Delete(int id)
		{
			var branch = await _context.Branches.FindAsync(id);

			if (branch == null)
			{
				return false;
			}

			_context.Branches.Remove(branch);

			await _context.SaveChangesAsync();

			return true;
		}

		
	}
}

