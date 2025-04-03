using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface ILeagueRepository : IBaseRepository<League>
    {
        Task<League> GetLeagueWithSeasonsAsync(int leagueId);
        Task<IEnumerable<League>> GetLeaguesByCountryAsync(string country);
        Task<bool> IsLeagueNameUniqueAsync(string name, int? excludeId = null);
    }
} 