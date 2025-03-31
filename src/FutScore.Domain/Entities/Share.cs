using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Share : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid PostId { get; set; }

        [StringLength(500)]
        public string? Message { get; set; }

        [StringLength(500)]
        public string? ShareUrl { get; set; }

        [StringLength(50)]
        public string? Platform { get; set; }

        // Navigation properties
        public virtual User? User { get; set; }
        public virtual Post? Post { get; set; }
    }
} 