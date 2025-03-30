using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserLeaderboard : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string LeaderboardType { get; set; }

        public int Rank { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int Draws { get; set; }

        public DateTime LastUpdatedAt { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
} 