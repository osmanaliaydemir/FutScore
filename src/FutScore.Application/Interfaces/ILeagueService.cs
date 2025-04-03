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
        Task<ProcessResult> CreateLeagueAsync(CreateLeagueDto leagueDto);
        Task<ProcessResult> UpdateLeagueAsync(UpdateLeagueDto leagueDto);
        Task<ProcessResult> DeleteLeagueAsync(int leagueId);
    }
}
