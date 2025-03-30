using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserAchievement : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string AchievementType { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string AchievementImageUrl { get; set; }
        public int Points { get; set; }
        public int Level { get; set; }

        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }

        public virtual User User { get; set; }
    }
} 