using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserVerification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 