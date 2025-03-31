using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserVerification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(500)]
        public string Token { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsVerified { get; set; }
        public DateTime? VerifiedAt { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 