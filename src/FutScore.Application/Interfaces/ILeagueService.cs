using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface ILeagueService : IBaseService<League>
    {
        Task<League> GetLeagueWithSeasonsAsync(int leagueId);
        Task<IEnumerable<League>> GetLeaguesByCountryAsync(string country);
        Task<bool> IsLeagueNameUniqueAsync(string name, int? excludeId = null);
    }
} 