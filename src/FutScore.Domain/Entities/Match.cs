using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Match : BaseEntity
    {
        [Required]
        public Guid SeasonId { get; set; }

        [Required]
        public Guid HomeTeamId { get; set; }

        [Required]
        public Guid AwayTeamId { get; set; }

        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        public string? Venue { get; set; }
        public string? Stadium { get; set; }

        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }

        public string? MatchStatus { get; set; }
        public string? MatchType { get; set; }
        public string? Competition { get; set; }

        public bool IsLive { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsCancelled { get; set; }

        // Navigation Properties
        public virtual Season? Season { get; set; }
        public virtual Team? HomeTeam { get; set; }
        public virtual Team? AwayTeam { get; set; }
        public virtual League? League { get; set; }
        public virtual ICollection<MatchEvent>? MatchEvents { get; set; }
        public virtual ICollection<Prediction>? Predictions { get; set; }
    }
} 