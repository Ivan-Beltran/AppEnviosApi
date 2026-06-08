using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPilotRepository
    {
        Task<Pilot?> GetByUserId(int userId);
    }
}