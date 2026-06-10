using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPilotRepository
    {
        Task<Pilot> Create(Pilot Pilot);

        Task<Pilot?> GetById(int id);

        Task<List<Pilot>> GetAll();

        Task<List<Pilot>> GetByBranchAsync(int branchId);

        Task<Pilot?> GetByIdAndBranchAsync(int id, int branchId);

        Task<Pilot> Update(Pilot Pilot);

        Task<bool> Delete(int id);
    }
}