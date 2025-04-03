using FutScore.Application.DTOs.Team;
using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByIdAsync(int id);
        Task<TeamDto> CreateTeamAsync(CreateTeamDto teamDto);
        Task UpdateTeamAsync(UpdateTeamDto teamDto);
        Task DeleteTeamAsync(int id);
        Task<Team> GetTeamWithPlayersAsync(int teamId);
        Task<Team> GetTeamWithMatchesAsync(int teamId);
        Task<Team> GetTeamWithStandingsAsync(int teamId);
        Task<IEnumerable<Team>> GetTeamsBySeasonAsync(int seasonId);
        Task<bool> IsTeamInSeasonAsync(int teamId, int seasonId);
        Task AddTeamToSeasonAsync(int teamId, int seasonId);
        Task RemoveTeamFromSeasonAsync(int teamId, int seasonId);
    }
} 