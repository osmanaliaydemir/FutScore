using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserBadge : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string BadgeType { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        public string? BadgeImageUrl { get; set; }
        public int Level { get; set; }

        public DateTime EarnedAt { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
} 