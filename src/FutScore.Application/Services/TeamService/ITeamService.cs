using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Services.GenericService;
using FutScore.Domain.Entities;

namespace FutScore.Application.Services.TeamService
{
    public interface ITeamService : IGenericService<TeamDto>
    {
        Task<TeamDetailDto> GetTeamDetailAsync(Guid id);
        Task<List<TeamListDto>> GetTeamListAsync(TeamFilterDto filter);
        Task<List<TeamDto>> GetTeamsByLeagueAsync(Guid leagueId);
        Task<List<PlayerDto>> GetTeamPlayersAsync(Guid teamId);
        Task<List<MatchDto>> GetTeamMatchesAsync(Guid teamId);
        Task<TeamStatsDto> GetTeamStatsAsync(Guid teamId);
        Task<bool> AddPlayerToTeamAsync(Guid teamId, Guid playerId);
        Task<bool> RemovePlayerFromTeamAsync(Guid teamId, Guid playerId);
        Task<bool> UpdateTeamStatsAsync(Guid teamId);
        Task<List<TeamDto>> GetTopTeamsAsync(int count);
        Task<List<TeamSeasonDto>> GetTeamSeasonsAsync(Guid teamId);
    }
} 