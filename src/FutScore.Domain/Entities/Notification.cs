using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Notification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Message { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [StringLength(1000)]
        public string? Data { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public virtual User? User { get; set; }
    }
} 