using System;
using System.Collections.Generic;

namespace FutScore.Application.DTOs.Match
{
    public class MatchLineupDto
    {
        public int Id { get; set; }
        public Guid MatchId { get; set; }
        public Guid TeamId { get; set; }
        public string TeamName { get; set; }
        public string Formation { get; set; }
        public List<PlayerLineupDto> Players { get; set; }
        public List<SubstituteLineupDto> Substitutes { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

    public class PlayerLineupDto
    {
        public int Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public int JerseyNumber { get; set; }
        public bool IsCaptain { get; set; }
        public bool IsStarting { get; set; }
    }

    public class SubstituteLineupDto
    {
        public int Id { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerName { get; set; }
        public string Position { get; set; }
        public int JerseyNumber { get; set; }
        public int? SubstitutionMinute { get; set; }
        public int? ReplacedPlayerId { get; set; }
        public string ReplacedPlayerName { get; set; }
    }
} 