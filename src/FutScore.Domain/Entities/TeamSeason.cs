using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class TeamSeason : BaseEntity
    {
        [Required]
        public Guid TeamId { get; set; }

        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        public Guid SeasonId { get; set; }

        [Required]
        [StringLength(20)]
        public string Season { get; set; }

        public int MatchesPlayed { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }

        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }

        public int Points { get; set; }
        public int Position { get; set; }

        public int CleanSheets { get; set; }
        public int FailedToScore { get; set; }

        public double WinPercentage { get; set; }
        public double GoalsPerGame { get; set; }
        public double GoalsConcededPerGame { get; set; }

        [StringLength(50)]
        public string? Form { get; set; }

        [StringLength(50)]
        public string? HomeForm { get; set; }

        [StringLength(50)]
        public string? AwayForm { get; set; }

        public bool IsPromoted { get; set; }
        public bool IsRelegated { get; set; }
        public bool IsQualifiedForEurope { get; set; }

        public virtual Team? Team { get; set; }
        public virtual League? League { get; set; }
    }
} 