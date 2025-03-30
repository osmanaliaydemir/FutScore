using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public string DeviceInfo { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        public virtual User User { get; set; }
    }
} 