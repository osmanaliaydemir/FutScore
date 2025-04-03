using FutScore.Application.DTOs.League;
using FutScore.Domain;
using FutScore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Interfaces
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDto>> GetAllLeaguesAsync();
        Task<ProcessResult> CreateLeagueAsync(LeagueDto leagueDto);
        Task<ProcessResult> UpdateLeagueAsync(LeagueDto leagueDto);
        Task<ProcessResult> DeleteLeagueAsync(int leagueId);
    }
}
