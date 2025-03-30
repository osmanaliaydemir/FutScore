using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;

namespace FutScore.Application.DTOs.Team
{
    public class TeamDetailDto 
    {
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public int Founded { get; set; }
        public string Logo { get; set; }
        public string Website { get; set; }
        public string Description { get; set; }
        public Guid LeagueId { get; set; }
        public int TotalMatches { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }
        public int GoalDifference { get; set; }
        public int Points { get; set; }
        public int Position { get; set; }

        // Navigation Properties
        public LeagueDto League { get; set; }
        public List<PlayerDto> Players { get; set; }
        public List<MatchDto> HomeMatches { get; set; }
        public List<MatchDto> AwayMatches { get; set; }
        public List<TeamSeasonDto> Seasons { get; set; }
    }
} 