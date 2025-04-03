using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface IStadiumRepository : IBaseRepository<Stadium>
    {
        Task<IEnumerable<Stadium>> GetStadiumsByCity(string city);
        Task<IEnumerable<Stadium>> GetStadiumsByCapacityRange(int minCapacity, int maxCapacity);
        Task<Stadium> GetStadiumWithMatches(int id);
    }
} 