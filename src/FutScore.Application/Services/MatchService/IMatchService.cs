using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.Services.GenericService;
using FutScore.Domain.Entities;

namespace FutScore.Application.Services.MatchService
{
    public interface IMatchService : IGenericService<MatchDto>
    {
        Task<MatchDetailDto> GetMatchDetailAsync(Guid id);
        Task<List<MatchListDto>> GetMatchListAsync(MatchFilterDto filter);
        Task<List<MatchDto>> GetUpcomingMatchesAsync();
        Task<List<MatchDto>> GetLiveMatchesAsync();
        Task<List<MatchDto>> GetFinishedMatchesAsync();
        Task<List<MatchDto>> GetMatchesByLeagueAsync(Guid leagueId);
        Task<List<MatchDto>> GetMatchesByTeamAsync(Guid teamId);
        Task<bool> UpdateMatchScoreAsync(Guid matchId, int? homeTeamScore, int? awayTeamScore);
        Task<bool> UpdateMatchStatusAsync(Guid matchId, string status);
        Task<List<MatchEventDto>> GetMatchEventsAsync(Guid matchId);
        Task<bool> AddMatchEventAsync(Guid matchId, MatchEventDto matchEvent);
        Task<bool> UpdateMatchEventAsync(Guid matchId, Guid eventId, MatchEventDto matchEvent);
        Task<bool> DeleteMatchEventAsync(Guid matchId, Guid eventId);
    }
} 