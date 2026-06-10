using Application.DTOs.Receiver;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Application.Services
{
    public class ReceiverService
    {
        private readonly IReceiverRepository _receiverRepository;

        public ReceiverService(IReceiverRepository receiverRepository)
        {
            _receiverRepository = receiverRepository;
        }

        public async Task<List<ReceiverDTO>> GetAllReceivers()
        {
            var receivers = await _receiverRepository.GetAll();
            return receivers.Select(r => new ReceiverDTO
            {
                Id = r.Id,
                FullName = r.FullName,
                Email = r.Email,
                Phone = r.Phone,
                Address=r.Address,
                DistrictId = r.DistrictId,
                DistrictName = r.District.DistrictName,
                CreatedByUserId = r.CreatedByUserId,
                CreatedByUserFullName = r.CreatedByUser.FullName,
            }).ToList();
        }

        public async Task<ReceiverDTO?> GetReceiverById(int id)
        {
            var receiver = await _receiverRepository.GetById(id);
            if (receiver == null) return null;
            return new ReceiverDTO
            {
                Id = receiver.Id,
                FullName = receiver.FullName,
                Email = receiver.Email,
                Phone = receiver.Phone,
                Address = receiver.Address,
                DistrictId = receiver.DistrictId,
                DistrictName = receiver.District.DistrictName,
                CreatedByUserId = receiver.CreatedByUserId,
                CreatedByUserFullName = receiver.CreatedByUser.FullName,
            };
        }

        public async Task<int> CreateReceiver(CreateReciverDTO dto, int createdByUserId)
        {
            var receiver = new Receiver
            {
                FullName = dto.FullName,
                Phone = dto.Phone,
                Email = dto.Email,
                Address = dto.Address,
                DistrictId = dto.DistrictId,
                CreatedByUserId = createdByUserId
            };

            await _receiverRepository.Create(receiver);

            return receiver.Id;
        }

        public async Task <int> UpdateReceiver(int id, UpdateReciverDTO dto)
            {
                var receiver = await _receiverRepository.GetById(id);
                if (receiver == null) return -1;
    
                receiver.FullName = dto.FullName;
                receiver.Phone = dto.Phone;
                receiver.Email = dto.Email;
                receiver.Address = dto.Address;
                receiver.DistrictId = dto.DistrictId;
    
                await _receiverRepository.Update(receiver);
                return receiver.Id;
        }

    }
}
