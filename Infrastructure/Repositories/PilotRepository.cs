using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PilotRepository : IPilotRepository
    {
        private readonly AppDbContext _context;

        public PilotRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Pilot> Create(Pilot pilot)
        {
            await _context.Pilots.AddAsync(pilot);
            await _context.SaveChangesAsync();

            return pilot;
        }

        public async Task<List<Pilot>> GetAll()
        {
            return await _context.Pilots
                .Include(a => a.User)
                .Include(a => a.Branch)
                .ToListAsync();
        }

        public async Task<Pilot?> GetById(int id)
        {
            return await _context.Pilots
                .Include(a => a.User)
                .Include(a => a.Branch)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Pilot> Update(Pilot pilot)
        {
            _context.Pilots.Update(pilot);
            await _context.SaveChangesAsync();

            return pilot;
        }

        public async Task<bool> Delete(int id)
        {
            var pilot = await _context.Pilots.FindAsync(id);

            if (pilot == null)
                return false;

            // no se borra de la BD
            pilot.IsActive = false;

            _context.Pilots.Update(pilot);
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

        public async Task<List<Pilot>> GetByBranchAsync(int branchId)
        {
            return await _context.Pilots
                .Include(p => p.User)
                .Include(p => p.Branch)
                .Where(p => p.BranchId == branchId)
                .ToListAsync();
        }

        public async Task<Pilot?> GetByIdAndBranchAsync(int id, int branchId)
        {
            return await _context.Pilots
                .Include(p => p.User)
                .Include(p => p.Branch)
                .FirstOrDefaultAsync(p =>
                    p.Id == id &&
                    p.BranchId == branchId);
        }
    }
}