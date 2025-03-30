
namespace FutScore.Application.DTOs.Match
{
    public class MatchListDto 
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string HomeTeamLogo { get; set; }
        public string AwayTeamLogo { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public DateTime MatchDate { get; set; }
        public string Venue { get; set; }
        public string Status { get; set; }
        public string LeagueName { get; set; }
        public string Season { get; set; }
        public string Round { get; set; }
        public int Attendance { get; set; }
        public string Referee { get; set; }
        public int HomeTeamYellowCards { get; set; }
        public int AwayTeamYellowCards { get; set; }
        public int HomeTeamRedCards { get; set; }
        public int AwayTeamRedCards { get; set; }
    }
} 