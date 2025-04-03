using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;

namespace FutScore.Application.DTOs.Season
{
    public class SeasonDto : BaseDto
    {
        public int Id { get; set; }
        public int LeagueId { get; set; }
        public string SeasonName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeagueDto League { get; set; }

    }
} 