using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Infrastructure.Data;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
namespace Infrastructure.Repositories
{
    public class CompanyClientRepository: ICompanyClient
    {
        private readonly AppDbContext _context;

        public CompanyClientRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CompanyClient> Create(CompanyClient companyClient)
        {
            await _context.CompanyClients.AddAsync(companyClient);
            await _context.SaveChangesAsync();
            return companyClient;
        }

        public async Task<List<CompanyClient>> GetAll()
        {
            return await _context.CompanyClients
                .Include(a => a.User)
                .Include(a => a.District)
                .ToListAsync();
        }

        public async Task<CompanyClient?> GetById(int id)
        {
            return await _context.CompanyClients
                .Include(a => a.User)
                .Include(a => a.District)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<CompanyClient> Update(CompanyClient companyClient)
        {
            _context.CompanyClients.Update(companyClient);
            await _context.SaveChangesAsync();
            return companyClient;
        }

        public async Task<bool> Delete(int id)
        {
            var companyClient = await _context.CompanyClients.FindAsync(id);
            if (companyClient == null)
                return false;
            companyClient.IsActive = false;
            _context.CompanyClients.Update(companyClient);
            await _context.SaveChangesAsync();

            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                user.IsActive = false;
                _context.Users.Update(user);
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
