using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserPermission : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Permission { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [StringLength(100)]
        public string? Resource { get; set; }

        [StringLength(100)]
        public string? Action { get; set; }

        public bool IsActive { get; set; }
        public DateTime? ExpiresAt { get; set; }

        [StringLength(100)]
        public string? GrantedBy { get; set; }
        public DateTime GrantedAt { get; set; }

        public virtual User? User { get; set; }
    }
} 