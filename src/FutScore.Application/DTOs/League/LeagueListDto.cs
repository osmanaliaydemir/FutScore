
namespace FutScore.Application.DTOs.League
{
    public class LeagueListDto 
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string Season { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int TotalTeams { get; set; }
        public int TotalMatches { get; set; }
        public int CompletedMatches { get; set; }
        public int UpcomingMatches { get; set; }
        public int TotalGoals { get; set; }
        public double AverageGoalsPerMatch { get; set; }
    }
} 