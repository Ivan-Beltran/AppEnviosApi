using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IShipmentConfirmationRepository
    {
        Task<ShipmentConfirmation> Create(ShipmentConfirmation confirmation);
    }
}