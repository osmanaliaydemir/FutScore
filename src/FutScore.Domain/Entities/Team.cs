using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Team : BaseEntity
    {
        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string ShortName { get; set; }

        [StringLength(200)]
        public string? Description { get; set; }

        public string? LogoUrl { get; set; }
        public string? BannerUrl { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(100)]
        public string? Stadium { get; set; }

        public int? FoundedYear { get; set; }

        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual League? League { get; set; }
        public virtual ICollection<TeamSeason>? TeamSeasons { get; set; }
        public virtual ICollection<Player>? Players { get; set; }
        public virtual ICollection<Match>? HomeMatches { get; set; }
        public virtual ICollection<Match>? AwayMatches { get; set; }
        public virtual ICollection<FavoriteTeam>? FavoriteTeams { get; set; }
    }
} 