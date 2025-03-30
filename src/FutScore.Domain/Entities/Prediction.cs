using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Prediction : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid MatchId { get; set; }

        [Required]
        public int PredictedHomeScore { get; set; }

        [Required]
        public int PredictedAwayScore { get; set; }

        public DateTime PredictedAt { get; set; }
        public bool IsCorrect { get; set; }
        public int PointsEarned { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
        public virtual Match? Match { get; set; }
    }
} 