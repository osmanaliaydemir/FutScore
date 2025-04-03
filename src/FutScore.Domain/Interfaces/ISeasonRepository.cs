using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface ISeasonRepository : IBaseRepository<Season>
    {
        Task<IEnumerable<Season>> GetAllWithLeaguesAsync();
    }
} 