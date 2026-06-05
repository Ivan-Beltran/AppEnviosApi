using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IAdminRepository
    {
        Task<Admin> Create(Admin admin);

        Task<Admin?> GetById(int id); 

        Task<List<Admin>> GetAll();

        Task<Admin> Update(Admin admin);

        Task<bool> Delete(int id);
    }
}
