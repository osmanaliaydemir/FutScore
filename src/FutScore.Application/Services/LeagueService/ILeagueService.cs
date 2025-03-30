using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Services.GenericService;
using FutScore.Domain;
using FutScore.Domain.Entities;

namespace FutScore.Application.Services.LeagueService
{
    public interface ILeagueService
    {
        // CRUD Operations
        Task<LeagueDto> CreateLeagueAsync(LeagueCreateDto createDto);
        Task<LeagueDto> UpdateLeagueAsync(Guid id, LeagueUpdateDto updateDto);
        Task<bool> DeleteLeagueAsync(Guid id);
        Task<LeagueDto> GetLeagueByIdAsync(Guid id);
        Task<IEnumerable<LeagueDto>> GetAllLeaguesAsync();
        
        // Additional Operations
        Task<List<LeagueListDto>> GetLeagueListAsync(LeagueFilterDto filter);
        Task<List<LeagueDto>> GetLeaguesByCountryAsync(string country);
        Task<bool> UpdateLeagueStatusAsync(Guid leagueId, string status);
        Task<ProcessResult> AddTeamToLeagueAsync(Guid leagueId, Guid teamId);
        Task<bool> RemoveTeamFromLeagueAsync(Guid leagueId, Guid teamId);
        Task<List<TeamDto>> GetLeagueTeamsAsync(Guid leagueId);
        Task<List<MatchDto>> GetLeagueMatchesAsync(Guid leagueId);
        Task<List<PlayerDto>> GetLeaguePlayersAsync(Guid leagueId);
        Task<LeagueStatsDto> GetLeagueStatsAsync(Guid leagueId);
    }
}
