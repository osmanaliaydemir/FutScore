

using FutScore.Application.DTOs.Prediction;
using FutScore.Application.Services.GenericService;

namespace FutScore.Application.Services.PredictionService
{
    public interface IPredictionService : IGenericService<PredictionDto>
    {
        Task<IEnumerable<PredictionDto>> GetAllAsync();
        Task<PredictionDto> GetByIdAsync(Guid id);
        Task<PredictionDto> CreateAsync(PredictionDto predictionDto);
        Task<PredictionDto> UpdateAsync(PredictionDto predictionDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<PredictionDto>> GetUserPredictionsAsync(Guid userId);
        Task<IEnumerable<PredictionDto>> GetMatchPredictionsAsync(Guid matchId);
        Task<PredictionStatisticsDto> GetUserPredictionStatisticsAsync(Guid userId);
        Task<IEnumerable<PredictionLeaderboardDto>> GetPredictionLeaderboardAsync();
    }
} 