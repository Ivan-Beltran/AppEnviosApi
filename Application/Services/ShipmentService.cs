using Application.DTOs.Shipment;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentConfirmationRepository _shipmentConfirmationRepository;
        private readonly IShipmentReturn _shipmentReturn;
  

        public ShipmentService(IShipmentRepository shipmentRepository, IShipmentConfirmationRepository shipmentConfirmationRepository, IShipmentReturn shipmentReturn)
        {
            _shipmentRepository = shipmentRepository;
            _shipmentConfirmationRepository = shipmentConfirmationRepository;
            _shipmentReturn = shipmentReturn;
        }

        public async Task<int> CreateAsync(CreateShipmentDTO dto, int userId)
        {
            var shipment = new Shipment
            {
                ReceiverId = dto.ReceiverId,
                DeliveryDate = dto.DeliveryDate,
                DeliveryAddress = dto.DeliveryAddress,
                DistrictDeliveryId = dto.DistrictDeliveryId,
                CreatedByUserId = userId,
                BranchId = dto.BranchId,
                StatusId = 1002,
                CreateAt = DateTime.UtcNow,

                Packages = dto.Packages.Select(p => new Package
                {
                    ProductName = p.ProductName,
                    Quantity = p.Quantity,
                    Description = p.Description
                }).ToList()
            };

            var shipmentCreate= await _shipmentRepository.CreateAsync(shipment);
            return shipmentCreate.Id;
        }

        public async Task<ShipmentDTO?> GetByIdAsync(int id)
        {
            var shipment= await _shipmentRepository.GetByIdAsync(id);
            if(shipment==null) return null;
            return new ShipmentDTO
            {
                Id = shipment.Id,
                ReceiverName = shipment.Receiver.FullName,
                DeliveryDate = shipment.DeliveryDate,
                DeliveryAddress = shipment.DeliveryAddress,
                DistrictDeliveryName = shipment.DistrictDelivery.DistrictName,
                CreatedByUserName = shipment.CreatedByUser.FullName,
                PilotName = shipment.Pilot.User.FullName,
                BranchName = shipment.Branch.BranchName,
                StatusName = shipment.ShipmentStatus.StatusName,
                CreateAt = shipment.CreateAt,
                UpdateAt = shipment.UpdateAt,


            };

        }

        public async Task<List<ShipmentDTO>> GetAllAsync(int roleId,int branchId,int userId)
        {
            List<Shipment> shipments;

            if (roleId == 1)
            {
                shipments = await _shipmentRepository.GetAllAsync();
            }
            else if (roleId == 2)
            {
                shipments = await _shipmentRepository
                    .GetByBranchAsync(branchId);
            }
            else if (roleId == 3)
            {
                shipments = await _shipmentRepository
                    .GetByPilotAsync(userId);
            }
            else
            {
                throw new UnauthorizedAccessException();
            }

            return shipments.Select(shipment => new ShipmentDTO
            {
                Id = shipment.Id,
                ReceiverName = shipment.Receiver?.FullName,
                DeliveryDate = shipment.DeliveryDate,
                DeliveryAddress = shipment.DeliveryAddress,
                DistrictDeliveryName =
                    shipment.DistrictDelivery?.DistrictName,
                CreatedByUserName =
                    shipment.CreatedByUser?.FullName,
                PilotName =
                    shipment.Pilot?.User?.FullName,
                BranchName =
                    shipment.Branch?.BranchName,
                StatusName =
                    shipment.ShipmentStatus?.StatusName,
                CreateAt = shipment.CreateAt,
                UpdateAt = shipment.UpdateAt
            }).ToList();
        }

        public async Task<Shipment> UpdateAsync(int id,  UpdateShipmentDTO dto)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(id);

            if (shipment == null)
                throw new Exception("Shipment no encontrado");

            shipment.DeliveryDate = dto.DeliveryDate;
            shipment.DeliveryAddress = dto.DeliveryAddress;
            shipment.DistrictDeliveryId = dto.DistrictDeliveryId;
            shipment.PilotId = dto.PilotId;
            shipment.BranchId = dto.BranchId;
            shipment.StatusId = dto.StatusId;
            shipment.UpdateAt = DateTime.UtcNow;

            return await _shipmentRepository.UpdateAsync(shipment);
        }

        public async Task UpdatePilotAsync(int shipmentId, UpdatePilotShipmentDTO dto)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);

            if (shipment == null)
                throw new Exception("Shipment no encontrado");

            shipment.PilotId = dto.pilotId;
            shipment.UpdateAt = DateTime.UtcNow;

            await _shipmentRepository.UpdateAsync(shipment);
        }

        public async Task UpdateStatusShipment(int shipmentId, UpdateStatusDTO dto)
        {
            var shipment = await _shipmentRepository.GetByIdAsync(shipmentId);

            if (shipment == null)
                throw new Exception("Shipment no encontrado");

            shipment.StatusId=dto.StatusId;
            shipment.UpdateAt = DateTime.UtcNow;

            await _shipmentRepository.UpdateAsync(shipment);

        }

        public async Task<int> CreateConfirmation(ConfirmationShipmentDTO dto)
        {
            var confirmation = new ShipmentConfirmation
            {
                ShipmentId = dto.ShipmentId,
                ReceiverSignature = dto.ReceiverSignature,
                ConfirmationPhoto = dto.ConfirmationPhoto,
                ConfirmationDate = DateTime.UtcNow
            };

            var createdConfirmation = await _shipmentConfirmationRepository.Create(confirmation);
            return createdConfirmation.Id;
        }

        public async Task<int> CreateReturn(CreateReturnDTO dto)
        {
            var shipmentReturn = new ShipmentReturn
            {
                ShipmentId = dto.ShipmentId,
                ReasonForReturn = dto.ReasonReturn,
                ReturnDate = DateTime.UtcNow
            };

            var createdReturn = await _shipmentReturn.Create(shipmentReturn);

            return createdReturn.Id;
        }



    }
}
