using FutScore.Application.DTOs.Season;
using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface ISeasonService
    {
        Task<IEnumerable<SeasonDto>> GetAllSeasonsAsync();
        Task<SeasonDto> GetSeasonByIdAsync(int id);
        Task<SeasonDto> CreateSeasonAsync(CreateSeasonDto seasonDto);
        Task UpdateSeasonAsync(UpdateSeasonDto seasonDto);
        Task DeleteSeasonAsync(int id);
        Task<Season> GetSeasonWithTeamsAsync(int seasonId);
        Task<Season> GetSeasonWithMatchesAsync(int seasonId);
        Task<Season> GetSeasonWithStandingsAsync(int seasonId);
        Task<IEnumerable<Season>> GetSeasonsByLeagueAsync(int leagueId);
        Task<IEnumerable<Season>> GetActiveSeasonsByLeagueAsync(int leagueId);
        Task<bool> HasOverlappingDatesAsync(int leagueId, DateTime startDate, DateTime endDate, int? excludeId = null);
    }
} 