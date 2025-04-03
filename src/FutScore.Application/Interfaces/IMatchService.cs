using FutScore.Application.DTOs.Match;
using FutScore.Domain;
using FutScore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Interfaces
{
    public interface IMatchService
    {
        Task<ProcessResult> AddMatchAsync(MatchDto matchDto);
        Task<ProcessResult> UpdateMatchAsync(MatchDto matchDto);
        Task<ProcessResult> DeleteMatchAsync(int id);
        Task<IEnumerable<MatchDto>> GetAllMatchesAsync();
        Task<MatchDto> GetMatchByIdAsync(int id);
    }
}
