using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface IStandingService : IBaseService<Standing>
    {
        Task<IEnumerable<Standing>> GetStandingsBySeasonAsync(int seasonId);
        Task<Standing> GetTeamStandingAsync(int seasonId, int teamId);
        Task UpdateStandingsAfterMatchAsync(int matchId);
        Task InitializeSeasonStandingsAsync(int seasonId);
        Task RecalculateSeasonStandingsAsync(int seasonId);
    }
} 