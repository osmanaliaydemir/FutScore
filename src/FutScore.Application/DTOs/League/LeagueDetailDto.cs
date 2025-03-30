using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Team;

namespace FutScore.Application.DTOs.League
{
    public class LeagueDetailDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Description { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public string Season { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int CompletedMatches { get; set; }
        public int UpcomingMatches { get; set; }
        public int TotalGoals { get; set; }
        public double AverageGoalsPerMatch { get; set; }

        // Navigation Properties
        public List<TeamDto> Teams { get; set; }
        public List<MatchDto> Matches { get; set; }
        public List<LeagueSeasonDto> Seasons { get; set; }
    }
} 