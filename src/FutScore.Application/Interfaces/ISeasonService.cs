using FutScore.Application.DTOs.Season;
using FutScore.Domain;
using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface ISeasonService
    {
        Task<IEnumerable<SeasonDto>> GetAllSeasonsAsync();
        Task<SeasonDto> GetByIdAsync(int id);
        Task<ProcessResult> CreateSeasonAsync(CreateSeasonDto seasonDto);
        Task<ProcessResult> UpdateSeasonAsync(UpdateSeasonDto seasonDto);
        Task<ProcessResult> DeleteSeasonAsync(int seasonId);
    }
} 