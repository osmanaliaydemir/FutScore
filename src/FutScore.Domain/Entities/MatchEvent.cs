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
        public string? PlayerName { get; set; }

        public Guid? TeamId { get; set; }
        public string? TeamName { get; set; }

        public string? Description { get; set; }
        public string? AdditionalData { get; set; }

        public virtual Match? Match { get; set; }
        public virtual Player? Player { get; set; }
        public virtual Team? Team { get; set; }
    }
} 