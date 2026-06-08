using Application.DTOs.Pilot;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PilotService
    {
        private readonly IPilotRepository _pilotRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentConfirmationRepository _confirmationRepository;

        public PilotService(
            IPilotRepository pilotRepository,
            IShipmentRepository shipmentRepository,
            IShipmentConfirmationRepository confirmationRepository)
        {
            _pilotRepository = pilotRepository;
            _shipmentRepository = shipmentRepository;
            _confirmationRepository = confirmationRepository;
        }

        public async Task<List<PilotShipmentDTO>> GetMyShipments(int userId)
        {
            var pilot = await _pilotRepository.GetByUserId(userId);
            if (pilot == null) return new List<PilotShipmentDTO>();

            var shipments = await _shipmentRepository.GetByPilotId(pilot.Id);

            return shipments.Select(s => new PilotShipmentDTO
            {
                ShipmentId = s.Id,
                ReceiverName = s.Receiver.FullName,
                ReceiverPhone = s.Receiver.Phone,
                DeliveryAddress = s.DeliveryAddress,
                District = s.DistrictDelivery.DistrictName,
                Status = s.ShipmentStatus.StatusName,
                DeliveryDate = s.DeliveryDate
            }).ToList();
        }

        public async Task<bool> UpdateShipmentStatus(int shipmentId, int userId, int newStatusId)
        {
            var pilot = await _pilotRepository.GetByUserId(userId);
            if (pilot == null) return false;

            var shipment = await _shipmentRepository.GetById(shipmentId);
            if (shipment == null || shipment.PilotId != pilot.Id) return false;

            shipment.StatusId = newStatusId;
            shipment.UpdateAt = DateTime.UtcNow;
            await _shipmentRepository.Update(shipment);
            return true;
        }

        public async Task<bool> ConfirmDelivery(int shipmentId, int userId, ConfirmDeliveryDTO dto)
        {
            var pilot = await _pilotRepository.GetByUserId(userId);
            if (pilot == null) return false;

            var shipment = await _shipmentRepository.GetById(shipmentId);
            if (shipment == null || shipment.PilotId != pilot.Id) return false;

            // Estado 4 = ENTREGADO
            shipment.StatusId = 4;
            shipment.UpdateAt = DateTime.UtcNow;
            await _shipmentRepository.Update(shipment);

            var confirmation = new ShipmentConfirmation
            {
                ShipmentId = shipmentId,
                ReceiverSignature = dto.ReceiverSignature,
                ConfirmationPhoto = dto.ConfirmationPhoto,
                ConfirmationDate = DateTime.UtcNow
            };

            await _confirmationRepository.Create(confirmation);
            return true;
        }

        public async Task<bool> ReportReturn(int shipmentId, int userId)
        {
            var pilot = await _pilotRepository.GetByUserId(userId);
            if (pilot == null) return false;

            var shipment = await _shipmentRepository.GetById(shipmentId);
            if (shipment == null || shipment.PilotId != pilot.Id) return false;

            // Estado 5 = DEVOLUCION
            shipment.StatusId = 5;
            shipment.UpdateAt = DateTime.UtcNow;
            await _shipmentRepository.Update(shipment);
            return true;
        }
    }
}