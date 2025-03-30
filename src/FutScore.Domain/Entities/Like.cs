using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Like : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid EntityId { get; set; }

        [Required]
        [StringLength(20)]
        public string EntityType { get; set; }

        // Navigation property
        public virtual User? User { get; set; }
    }
} 