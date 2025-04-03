using FutScore.Domain.Entities;
using FutScore.Application.DTOs.Match;

namespace FutScore.Application.Interfaces
{
    public interface IMatchService : IBaseService<Match>
    {
        Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId);
        Task<IEnumerable<Match>> GetUpcomingMatchesAsync(int? seasonId = null);
        Task<IEnumerable<Match>> GetCompletedMatchesAsync(int? seasonId = null);
        Task UpdateMatchScoreAsync(int matchId, int homeScore, int awayScore);
        Task FinishMatchAsync(int matchId);
        Task<bool> IsMatchTimeValidAsync(int seasonId, DateTime matchDate);
        Task<bool> AreTeamsAvailableAsync(int seasonId, int homeTeamId, int awayTeamId, DateTime matchDate);
        Task<IEnumerable<MatchDto>> GetAllMatchesAsync();
        Task<MatchDto> GetMatchByIdAsync(int id);
        Task<MatchDto> CreateMatchAsync(CreateMatchDto matchDto);
        Task UpdateMatchAsync(UpdateMatchDto matchDto);
        Task DeleteMatchAsync(int id);
    }
} 