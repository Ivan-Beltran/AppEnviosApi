using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICompanyClient
    {
        Task<CompanyClient> Create(CompanyClient companyClient);

        Task<CompanyClient?> GetById(int id);

        Task<List<CompanyClient>> GetAll();
        
        Task<CompanyClient> Update(CompanyClient companyClient);

        Task<bool> Delete(int id);
    }
}
