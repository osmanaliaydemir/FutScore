using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserSession : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Token { get; set; }

        [StringLength(500)]
        public string? DeviceInfo { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        public DateTime LastActivityAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 