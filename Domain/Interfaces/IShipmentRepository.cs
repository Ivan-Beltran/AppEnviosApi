using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShipmentRepository
    {
        Task<List<Shipment>> GetByPilotId(int pilotId);
        Task<Shipment?> GetById(int shipmentId);
        Task<Shipment> Update(Shipment shipment);
    }
}