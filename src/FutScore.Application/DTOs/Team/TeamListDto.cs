using FutScore.Application.DTOs.League;

namespace FutScore.Application.DTOs.Team
{
    public class TeamListDto
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Logo { get; set; }
        public Guid LeagueId { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }
        public int GoalDifference { get; set; }

        // Navigation Properties
        public LeagueDto League { get; set; }
    }
} 