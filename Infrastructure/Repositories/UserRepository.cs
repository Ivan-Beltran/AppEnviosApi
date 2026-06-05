using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<User> Create(User user)
        {
            await _context.Users.AddAsync(user);

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetByEmailWithRole(string email)
        {
            return await _context.Users
                .AsNoTracking() 
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetById(int id)
        {
            return await _context.Users
                .AsNoTracking()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> Update(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            // Aquí es donde realmente se manda a llamar al motor de EF Core
            return await _context.Database.BeginTransactionAsync();
        }


    }
}
