using System;

namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionListDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string MatchTitle { get; set; }
        public string PredictedScore { get; set; }
        public string PredictedResult { get; set; }
        public int Points { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
    }
} 