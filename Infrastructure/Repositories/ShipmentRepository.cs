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

        public async Task<Shipment> CreateAsync(Shipment shipment)
        {
            await _context.Shipments.AddAsync(shipment);
            await _context.SaveChangesAsync();

            return shipment;
        }

        public async Task<List<Shipment>> GetAllAsync()
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.DistrictDelivery)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Pilot)
                     .ThenInclude(p => p.User)
                .Include(s => s.Branch)
                .Include(s => s.ShipmentStatus)
                .ToListAsync();
        }

        public async Task<Shipment?> GetByIdAsync(int id)
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.DistrictDelivery)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Pilot)
                        .ThenInclude(p => p.User)
                .Include(s => s.Branch)
                .Include(s => s.ShipmentStatus)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Shipment> UpdateAsync(Shipment shipment)
        {
            _context.Shipments.Update(shipment);
            await _context.SaveChangesAsync();

            return shipment;
        }

        public async Task<List<Shipment>> GetByBranchAsync(int branchId)
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.DistrictDelivery)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Pilot)
                     .ThenInclude(p => p.User)
                .Include(s => s.Branch)
                .Include(s => s.ShipmentStatus)
                .Where(p => p.BranchId == branchId)
                .ToListAsync();
        }

        public async Task<List<Shipment>> GetByPilotAsync(int pilotId)
        {
            return await _context.Shipments
                .Include(s => s.Receiver)
                .Include(s => s.DistrictDelivery)
                .Include(s => s.CreatedByUser)
                .Include(s => s.Pilot)
                    .ThenInclude(p => p.User)
                .Include(s => s.Branch)
                .Include(s => s.ShipmentStatus)
                .Where(s => s.PilotId == pilotId)
                .ToListAsync();
        }

    }
}