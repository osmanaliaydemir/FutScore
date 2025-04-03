using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId);
        Task<IEnumerable<Match>> GetUpcomingMatchesAsync(int? seasonId = null);
        Task<IEnumerable<Match>> GetCompletedMatchesAsync(int? seasonId = null);
        Task UpdateMatchScoreAsync(int matchId, int homeScore, int awayScore);
        Task FinishMatchAsync(int matchId);
        Task<bool> IsMatchTimeValidAsync(int seasonId, DateTime matchDate);
        Task<bool> AreTeamsAvailableAsync(int seasonId, int homeTeamId, int awayTeamId, DateTime matchDate);
    }
} 