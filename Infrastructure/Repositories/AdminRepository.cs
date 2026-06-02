using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDbContext _context;
        public AdminRepository(AppDbContext context)
        {
            _context = context;
        }
        
        public async Task<Admin> Create(Admin admin)
        {
            await _context.Admins.AddAsync(admin);

            await _context.SaveChangesAsync();

            return admin;
        }
    }
}
