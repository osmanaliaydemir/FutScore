using System;

namespace FutScore.Application.DTOs.User
{
    public class UserStatisticsDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public int WrongPredictions { get; set; }
        public double SuccessRate { get; set; }
        public int TotalPoints { get; set; }
        public double AveragePoints { get; set; }
        public int MaxPoints { get; set; }
        public int MinPoints { get; set; }
        public int StreakCount { get; set; }
        public int MaxStreak { get; set; }
        public DateTime LastPredictionAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public UserDto User { get; set; }
    }
} 