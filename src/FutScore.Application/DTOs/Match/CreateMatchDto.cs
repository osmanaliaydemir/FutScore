using FutScore.Application.DTOs.Stadium;

namespace FutScore.Application.DTOs.Match
{
    public class CreateMatchDto
    {
        public int SeasonId { get; set; }
        public int StadiumId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public string Status { get; set; }

        public ICollection<StadiumDto>? Stadium { get; set; }
    }
}