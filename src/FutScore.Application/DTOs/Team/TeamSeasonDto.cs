

namespace FutScore.Application.DTOs.Team
{
    public class TeamSeasonDto 
    {
        public Guid TeamId { get; set; }
        public string Season { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public string Form { get; set; }
        public bool IsCurrentSeason { get; set; }

        // Navigation Properties
        public TeamDto Team { get; set; }
    }
} 