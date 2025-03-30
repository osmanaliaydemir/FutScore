using System;

namespace  FutScore.Application.DTOs.League
{
    public class LeagueSeasonDto
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string LeagueName { get; set; }
        public string Season { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int CompletedMatches { get; set; }
        public int RemainingMatches { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
} 