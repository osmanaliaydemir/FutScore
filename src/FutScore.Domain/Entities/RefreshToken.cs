using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class RefreshToken : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Token { get; set; }

        [StringLength(500)]
        public string? DeviceInfo { get; set; }

        [StringLength(50)]
        public string? IpAddress { get; set; }

        [StringLength(500)]
        public string? UserAgent { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsRevoked { get; set; }

        public virtual User? User { get; set; }
    }
} 