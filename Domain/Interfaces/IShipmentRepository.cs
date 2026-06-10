using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShipmentRepository
    {
        Task<Shipment> CreateAsync(Shipment shipment);
        Task<Shipment?> GetByIdAsync(int id);
        Task<List<Shipment>> GetAllAsync();
        Task<Shipment> UpdateAsync(Shipment shipment);

        Task<List<Shipment>> GetByBranchAsync(int branchId);

        Task<List<Shipment>> GetByPilotAsync(int pilotId);
    }
}