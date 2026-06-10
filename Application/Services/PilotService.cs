using Application.DTOs.AdminArea;
using Application.DTOs.Pilot;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class PilotService
    {
        private readonly IPilotRepository _pilotRepository;
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IShipmentConfirmationRepository _confirmationRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordService _passwordService;

        public PilotService(
            IPilotRepository pilotRepository,
            IShipmentRepository shipmentRepository,
            IShipmentConfirmationRepository confirmationRepository,
            IUserRepository userRepository, 
            IPasswordService passwordService)
        {
            _pilotRepository = pilotRepository;
            _shipmentRepository = shipmentRepository;
            _confirmationRepository = confirmationRepository;
            _userRepository = userRepository;
            _passwordService = passwordService;
            
        }


        public async Task<List<PilotDTO>> GetAllAsync(int roleId,int branchId)
        {
            List<Pilot> pilots;

            if (roleId == 1)
            {
                pilots = await _pilotRepository.GetAll();
            }
            else
            {
                pilots = await _pilotRepository.GetByBranchAsync(branchId);
            }

            return pilots.Select(p => new PilotDTO
            {
                UserId = p.Id,
                FullName = p.User.FullName,
                Email = p.User.Email,
                BranchName = p.Branch.BranchName,
                CreatedAt=p.CreatedAt,
                LicenseNumber=p.LicenseNumber,
                BranchId=p.BranchId,
                IsActive=p.IsActive
            }).ToList();
        }

        public async Task<PilotDTO?> GetByIdAsync(
            int id,
            int roleId,
            int branchId)
        {
            Pilot? pilot;

            if (roleId == 1)
            {
                pilot = await _pilotRepository.GetById(id);
            }
            else
            {
                pilot = await _pilotRepository
                    .GetByIdAndBranchAsync(id, branchId);
            }

            if (pilot == null)
                return null;

            return new PilotDTO
            {
                UserId = pilot.Id,
                FullName = pilot.User.FullName,
                Email = pilot.User.Email,
                BranchName = pilot.Branch.BranchName,
                CreatedAt = pilot.CreatedAt,
                LicenseNumber = pilot.LicenseNumber,
                BranchId = pilot.BranchId,
                IsActive = pilot.IsActive
            };
        }

        public async Task<int> CreatePilot(CreatePilotDTO dto)
        {
            var (passwordHash, salt) = _passwordService.HashPassword(dto.Password);
            User userBase = new User()
            {
                FullName = dto.FullName,
                Phone = dto.PhoneNumber,
                Email = dto.Email,
                Password = passwordHash,
                Salt = salt,
                RoleId = 3,
                IsActive = true
            };

            var createdUser = await _userRepository.Create(userBase);

            Pilot pilot = new Pilot()
            {
                Id = createdUser.Id,
                BranchId = dto.BranchId,
                LicenseNumber = dto.LicenseNumber,
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            await _pilotRepository.Create(pilot);

            return createdUser.Id;
        }

        public async Task<int> UpdatePilot(int id, UpdatePilotDTO dto, int roleId, int branchId)
        {
            using var transaction = await _userRepository.BeginTransactionAsync();

            try
            {
                var user = await _userRepository.GetById(id);
                if (user == null || user.RoleId != 3)
                    return -1;

                var pilot = await _pilotRepository.GetById(id);
                if (pilot == null)
                    return -1;

                // Si es AreaAdmin (roleId == 2), solo puede editar pilotos de su sucursal
                if (roleId == 2 && pilot.BranchId != branchId)
                    return -2; // código para "sin permiso"

                user.FullName = dto.FullName;
                user.Phone = dto.PhoneNumber;
                user.Email = dto.Email;
                await _userRepository.Update(user);

                pilot.BranchId = dto.BranchId;
                await _pilotRepository.Update(pilot);

                await transaction.CommitAsync();
                return user.Id;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<bool> DeletePilot(int id)
        {
            return await _pilotRepository.Delete(id);
        }









    }
}