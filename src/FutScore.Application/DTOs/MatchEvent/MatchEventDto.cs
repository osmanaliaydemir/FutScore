using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using System;

namespace FutScore.Application.DTOs.MatchEvent
{
    public class MatchEventDto
    {
        public Guid Id { get; set; }
        public Guid MatchId { get; set; }
        public string EventType { get; set; }
        public int Minute { get; set; }
        public int? ExtraMinute { get; set; }
        public bool IsFirstHalf { get; set; }
        public Guid? PlayerId { get; set; }
        public string PlayerName { get; set; }
        public Guid? TeamId { get; set; }
        public string TeamName { get; set; }
        public string Description { get; set; }
        public string AdditionalData { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public MatchDto Match { get; set; }
        public PlayerDto Player { get; set; }
        public TeamDto Team { get; set; }
    }
} 