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
        public string Message { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public string Data { get; set; }
        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        public virtual User User { get; set; }
    }
} 