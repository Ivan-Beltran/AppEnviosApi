using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly AppDbContext _context;

        public ShipmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Shipment>> GetByPilotId(int pilotId)
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.ShipmentStatus)
                .Include(s => s.DistrictDelivery)
                .Where(s => s.PilotId == pilotId)
                .ToListAsync();
        }

        public async Task<Shipment?> GetById(int shipmentId)
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.ShipmentStatus)
                .FirstOrDefaultAsync(s => s.Id == shipmentId);
        }

        public async Task<Shipment> Update(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();
            return shipment;
        }
    }
}