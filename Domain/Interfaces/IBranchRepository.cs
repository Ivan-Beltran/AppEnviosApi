using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IBranchRepository
    {
		Task<List<Branch>> GetAll();

		Task<Branch?> GetById(int id);

		Task<Branch> Create(Branch branch);

		Task<Branch?> Update(int id, Branch branch);

		Task<bool> Delete(int id);
	}
}
