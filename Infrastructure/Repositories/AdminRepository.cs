using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
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
            // Nota: Si usas transacciones en el servicio, el SaveChangesAsync de aquí se unirá a ella automáticamente.
            await _context.Admins.AddAsync(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<List<Admin>> GetAll()
        {
            // OPTIMIZACIÓN: Traer también la información de la tabla User vinculada
            return await _context.Admins
                .Include(a => a.User) // Carga la relación con la tabla base
                .ToListAsync();
        }

        public async Task<Admin?> GetById(int id)
        {
            // OPTIMIZACIÓN: Usar FirstOrDefaultAsync con Include en lugar de FindAsync
            return await _context.Admins
                .Include(a => a.User)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Admin> Update(Admin admin)
        {
            _context.Admins.Update(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<bool> Delete(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
                return false;

            
            admin.IsActive = false;
            _context.Admins.Update(admin);

            
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
