using AutoMapper;
using FutScore.Application.DTOs.Prediction;
using FutScore.Application.Services.GenericService;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.PredictionService
{
    public class PredictionService : IPredictionService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PredictionService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ProcessResult> AddAsync(PredictionDto entity)
        {
            try
            {
                var prediction = _mapper.Map<Prediction>(entity);
                await _context.Predictions.AddAsync(prediction);
                await _context.SaveChangesAsync();
                return new ProcessResult { Success = true, Message = "Prediction added successfully" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<PredictionDto> CreateAsync(PredictionDto predictionDto)
        {
            var prediction = _mapper.Map<Prediction>(predictionDto);
            await _context.Predictions.AddAsync(prediction);
            await _context.SaveChangesAsync();
            return _mapper.Map<PredictionDto>(prediction);
        }

        public async Task DeleteAsync(int id)
        {
            var prediction = await _context.Predictions.FindAsync(id);
            if (prediction != null)
            {
                _context.Predictions.Remove(prediction);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<ProcessResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var prediction = await _context.Predictions.FindAsync(id);
                if (prediction != null)
                {
                    _context.Predictions.Remove(prediction);
                    await _context.SaveChangesAsync();
                    return new ProcessResult { Success = true, Message = "Prediction deleted successfully" };
                }
                return new ProcessResult { Success = false, Message = "Prediction not found" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = ex.Message };
            }
        }

        public async Task<IEnumerable<PredictionDto>> GetAllAsync()
        {
            var predictions = await _context.Predictions.ToListAsync();
            return _mapper.Map<IEnumerable<PredictionDto>>(predictions);
        }

        public async Task<PredictionDto> GetByIdAsync(Guid id)
        {
            var prediction = await _context.Predictions.FindAsync(id);
            return _mapper.Map<PredictionDto>(prediction);
        }

        public async Task<IEnumerable<PredictionDto>> GetMatchPredictionsAsync(Guid matchId)
        {
            var predictions = await _context.Predictions
                .Where(p => p.MatchId == matchId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PredictionDto>>(predictions);
        }

        public async Task<IEnumerable<PredictionLeaderboardDto>> GetPredictionLeaderboardAsync()
        {
            var predictions = await _context.Predictions
                .Include(p => p.User)
                .ToListAsync();

            return predictions
                .GroupBy(p => p.UserId)
                .Select(g => new PredictionLeaderboardDto
                {
                    UserId = g.Key,
                    UserName = g.First().User?.Username ?? "Unknown",
                    TotalPoints = g.Sum(p => p.PointsEarned),
                    CorrectPredictions = g.Count(p => p.PointsEarned > 0),
                    TotalPredictions = g.Count(),
                    SuccessRate = g.Count() > 0 ? (double)g.Count(p => p.PointsEarned > 0) / g.Count() : 0
                })
                .OrderByDescending(p => p.TotalPoints)
                .Take(10);
        }

        public async Task<IEnumerable<PredictionDto>> GetUserPredictionsAsync(Guid userId)
        {
            var predictions = await _context.Predictions
                .Where(p => p.UserId == userId)
                .ToListAsync();
            return _mapper.Map<IEnumerable<PredictionDto>>(predictions);
        }

        public async Task<PredictionStatisticsDto> GetUserPredictionStatisticsAsync(Guid userId)
        {
            var predictions = await _context.Predictions
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return new PredictionStatisticsDto
            {
                TotalPredictions = predictions.Count,
                CorrectPredictions = predictions.Count(p => p.PointsEarned > 0),
                TotalPoints = predictions.Sum(p => p.PointsEarned),
                AveragePoints = predictions.Any() ? predictions.Average(p => p.PointsEarned) : 0
            };
        }

        public async Task<PredictionDto> UpdateAsync(PredictionDto predictionDto)
        {
            var prediction = await _context.Predictions.FindAsync(predictionDto.Id);
            if (prediction != null)
            {
                _mapper.Map(predictionDto, prediction);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<PredictionDto>(prediction);
        }

        async Task<List<PredictionDto>> IGenericService<PredictionDto>.GetAllAsync()
        {
            var predictions = await _context.Predictions.ToListAsync();
            return _mapper.Map<List<PredictionDto>>(predictions);
        }

        async Task<ProcessResult> IGenericService<PredictionDto>.UpdateAsync(PredictionDto entity)
        {
            try
            {
                var prediction = await _context.Predictions.FindAsync(entity.Id);
                if (prediction != null)
                {
                    _mapper.Map(entity, prediction);
                    await _context.SaveChangesAsync();
                    return new ProcessResult { Success = true, Message = "Prediction updated successfully" };
                }
                return new ProcessResult { Success = false, Message = "Prediction not found" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = ex.Message };
            }
        }
    }
}