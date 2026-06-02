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
		Task<List<Branch>> ObtenerTodos();

		Task<Branch?> ObtenerPorId(int id);

		Task<Branch> Crear(Branch branch);

		Task<Branch?> Actualizar(int id, Branch branch);

		Task<bool> Eliminar(int id);
	}
}
