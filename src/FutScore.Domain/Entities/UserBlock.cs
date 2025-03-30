using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserBlock : BaseEntity
    {
        [Required]
        public Guid BlockerId { get; set; }

        [Required]
        public Guid BlockedId { get; set; }

        [StringLength(200)]
        public string? Reason { get; set; }

        public DateTime? ExpiresAt { get; set; }
        public bool IsPermanent { get; set; }

        // Navigation Properties
        public virtual User? Blocker { get; set; }
        public virtual User? Blocked { get; set; }
    }
} 