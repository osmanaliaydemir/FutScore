using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserReward : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string RewardType { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Points { get; set; }
        public string RewardData { get; set; }

        public bool IsClaimed { get; set; }
        public DateTime? ClaimedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
} 