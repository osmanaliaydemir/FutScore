using FutScore.Application.DTOs.Team;
using FutScore.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllTeamsAsync();
        Task<TeamDto> GetTeamByIdAsync(int id);
        Task<ProcessResult> CreateTeamAsync(CreateTeamDto teamDto);
        Task<ProcessResult> UpdateTeamAsync(UpdateTeamDto teamDto);
        Task<ProcessResult> DeleteTeamAsync(int teamId);
    }
}
