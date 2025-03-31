using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class MatchEvent : BaseEntity
    {
        [Required]
        public Guid MatchId { get; set; }

        [Required]
        [StringLength(50)]
        public string EventType { get; set; }

        [Required]
        public int Minute { get; set; }

        public int? ExtraMinute { get; set; }
        public bool IsFirstHalf { get; set; }

        public Guid? PlayerId { get; set; }

        [StringLength(100)]
        public string? PlayerName { get; set; }

        public Guid? TeamId { get; set; }

        [StringLength(100)]
        public string? TeamName { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [StringLength(1000)]
        public string? AdditionalData { get; set; }

        public virtual Match? Match { get; set; }
        public virtual Player? Player { get; set; }
        public virtual Team? Team { get; set; }
    }
} 