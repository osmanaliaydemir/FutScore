using FutScore.Domain.Entities;

namespace FutScore.Domain.Interfaces
{
    public interface ITeamRepository : IBaseRepository<Team>
    {
        Task<Team> GetTeamWithPlayersAsync(int teamId);
        Task<Team> GetTeamWithMatchesAsync(int teamId);
        Task<Team> GetTeamWithSeasonsAsync(int teamId);
        Task<Team> GetTeamWithStandingsAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsBySeasonAsync(int seasonId);
        Task<bool> IsTeamInSeasonAsync(int teamId, int seasonId);
        Task AddTeamToSeasonAsync(int teamId, int seasonId);
        Task RemoveTeamFromSeasonAsync(int teamId, int seasonId);
    }
} 