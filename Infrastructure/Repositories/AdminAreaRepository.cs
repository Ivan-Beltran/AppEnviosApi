using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
	public class AdminAreaRepository : IAdminAreaRepository
	{
		private readonly AppDbContext _context;

		public AdminAreaRepository(AppDbContext context)
		{
			_context = context;
		}

		public async Task<AdminArea> Create(AdminArea adminArea)
		{
			await _context.AdminAreas.AddAsync(adminArea);
			await _context.SaveChangesAsync();

			return adminArea;
		}

		public async Task<List<AdminArea>> GetAll()
		{
			return await _context.AdminAreas
				.Include(a => a.User)
				.Include(a => a.Branch)
				.ToListAsync();
		}

		public async Task<AdminArea?> GetById(int id)
		{
			return await _context.AdminAreas
				.Include(a => a.User)
				.Include(a => a.Branch)
				.FirstOrDefaultAsync(a => a.Id == id);
		}

		public async Task<AdminArea> Update(AdminArea adminArea)
		{
			_context.AdminAreas.Update(adminArea);
			await _context.SaveChangesAsync();

			return adminArea;
		}

		public async Task<bool> Delete(int id)
		{
			var adminArea = await _context.AdminAreas.FindAsync(id);

			if (adminArea == null)
				return false;

			// no se borra de la BD
			adminArea.IsActive = false;

			_context.AdminAreas.Update(adminArea);
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
