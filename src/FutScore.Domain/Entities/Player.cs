using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Player : BaseEntity
    {
        [Required]
        public Guid TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string? Number { get; set; }

        [StringLength(50)]
        public string? Position { get; set; }

        public DateTime? DateOfBirth { get; set; }
        public string? Nationality { get; set; }

        public string? PhotoUrl { get; set; }

        public bool IsActive { get; set; }
        public bool IsInjured { get; set; }
        public bool IsSuspended { get; set; }

        // Navigation Properties
        public virtual Team? Team { get; set; }
        public virtual ICollection<PlayerSeason>? PlayerSeasons { get; set; }
        public virtual ICollection<MatchEvent>? MatchEvents { get; set; }
    }
} 