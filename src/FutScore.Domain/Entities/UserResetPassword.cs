using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserResetPassword : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        public string Email { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsUsed { get; set; }
        public DateTime? UsedAt { get; set; }

        public virtual User User { get; set; }
    }
} 