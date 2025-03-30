using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.User;
using System;

namespace FutScore.Application.DTOs.Prediction
{
    public class PredictionDetailDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public Guid MatchId { get; set; }
        public string MatchTitle { get; set; }
        public string PredictedScore { get; set; }
        public string PredictedResult { get; set; }
        public int Points { get; set; }
        public bool IsCorrect { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public UserDto User { get; set; }
        public MatchDto Match { get; set; }
    }
} 