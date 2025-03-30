using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserProgress : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string ProgressType { get; set; }

        public int CurrentLevel { get; set; }
        public int CurrentPoints { get; set; }
        public int TotalPoints { get; set; }
        public int StreakCount { get; set; }
        public int MaxStreak { get; set; }

        public DateTime? LastActivityDate { get; set; }
        public DateTime? LastRewardClaimedAt { get; set; }

        public virtual User? User { get; set; }
    }
} 