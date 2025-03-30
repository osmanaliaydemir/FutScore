using System;

namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionFilterDto
    {
        public Guid? UserId { get; set; }
        public Guid? MatchId { get; set; }
        public bool? IsCorrect { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MinPoints { get; set; }
        public int? MaxPoints { get; set; }
        public string PredictedResult { get; set; }
        public bool IncludeDeleted { get; set; }
    }
} 