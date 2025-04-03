using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        Task<IEnumerable<Team>> GetAllWithRelationsAsync();
    }
}
