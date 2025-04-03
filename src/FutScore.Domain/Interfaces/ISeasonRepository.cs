using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface ISeasonRepository : IBaseRepository<Season>
    {
        Task<Season> GetSeasonWithTeamsAsync(int seasonId);
        Task<Season> GetSeasonWithMatchesAsync(int seasonId);
        Task<Season> GetSeasonWithStandingsAsync(int seasonId);
        Task<IEnumerable<Season>> GetSeasonsByLeagueAsync(int leagueId);
        Task<IEnumerable<Season>> GetActiveSeasonsByLeagueAsync(int leagueId);
        Task<bool> HasOverlappingDatesAsync(int leagueId, DateTime startDate, DateTime endDate, int? excludeId = null);
    }
} 