using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Interfaces
{
	public interface IAdminAreaRepository
	{
		Task<AdminArea> Create(AdminArea adminArea);

		Task<AdminArea?> GetById(int id);

		Task<List<AdminArea>> GetAll();

		Task<AdminArea> Update(AdminArea adminArea);

		Task<bool> Delete(int id);
	}
}
