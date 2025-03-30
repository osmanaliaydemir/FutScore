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

        public string DeviceInfo { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public DateTime LastActivityAt { get; set; }
        public DateTime ExpiresAt { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
} 