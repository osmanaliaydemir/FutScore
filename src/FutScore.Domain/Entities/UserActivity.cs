using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserActivity : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? Data { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
} 