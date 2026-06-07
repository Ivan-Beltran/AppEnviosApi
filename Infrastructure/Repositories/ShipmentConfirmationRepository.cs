using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ShipmentConfirmationRepository : IShipmentConfirmationRepository
    {
        private readonly AppDbContext _context;

        public ShipmentConfirmationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ShipmentConfirmation> Create(ShipmentConfirmation confirmation)
        {
            _context.ShipmentConfirmations.Add(confirmation);
            await _context.SaveChangesAsync();
            return confirmation;
        }
    }
}