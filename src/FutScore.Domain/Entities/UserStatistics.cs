using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserStatistics : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        public int TotalMatches { get; set; }
        public int WonMatches { get; set; }
        public int LostMatches { get; set; }
        public int DrawMatches { get; set; }

        public int TotalPredictions { get; set; }
        public int CorrectPredictions { get; set; }
        public int WrongPredictions { get; set; }

        public int TotalPoints { get; set; }
        public int CurrentStreak { get; set; }
        public int BestStreak { get; set; }

        public DateTime LastMatchDate { get; set; }
        public DateTime LastPredictionDate { get; set; }

        public virtual User User { get; set; }
    }
} 