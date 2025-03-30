using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class League : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }
        public string? MatchStatus { get; set; }

        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        // Navigation Properties
        public virtual ICollection<Season>? Seasons { get; set; }
        public virtual ICollection<Team>? Teams { get; set; }
        public virtual ICollection<Match>? Matches { get; set; }
    }
} 