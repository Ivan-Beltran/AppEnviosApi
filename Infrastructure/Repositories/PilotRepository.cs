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

        public async Task<Pilot?> GetByUserId(int userId)
        {
            return await _context.Pilots
                .Include(p => p.User)
                .Include(p => p.Branch)
                .FirstOrDefaultAsync(p => p.Id == userId);
        }
    }
}