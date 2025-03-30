using System;

namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MatchId { get; set; }
        public int PredictedHomeScore { get; set; }
        public int PredictedAwayScore { get; set; }
        public int Points { get; set; }
        public string UserName { get; set; }
        public string MatchInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 