using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.DTOs.Prediction;
using FutScore.Application.DTOs.Team;
using FutScore.Domain.Entities;

namespace FutScore.Application.DTOs.Match
{
    public class MatchDetailDto 
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
        public string Referee { get; set; }
        public int Attendance { get; set; }
        public string LeagueName { get; set; }
        public string Season { get; set; }
        public string Round { get; set; }
        public int HomeTeamYellowCards { get; set; }
        public int AwayTeamYellowCards { get; set; }
        public int HomeTeamRedCards { get; set; }
        public int AwayTeamRedCards { get; set; }
        public int HomeTeamCorners { get; set; }
        public int AwayTeamCorners { get; set; }
        public int HomeTeamPossession { get; set; }
        public int AwayTeamPossession { get; set; }
        public int HomeTeamShotsOnTarget { get; set; }
        public int AwayTeamShotsOnTarget { get; set; }
        public int HomeTeamShotsOffTarget { get; set; }
        public int AwayTeamShotsOffTarget { get; set; }
        public int HomeTeamFouls { get; set; }
        public int AwayTeamFouls { get; set; }
        public int HomeTeamOffsides { get; set; }
        public int AwayTeamOffsides { get; set; }
        public int HomeTeamSubstitutions { get; set; }
        public int AwayTeamSubstitutions { get; set; }
        public string HomeTeamFormation { get; set; }
        public string AwayTeamFormation { get; set; }
        public List<MatchEventDto> Events { get; set; }
        public List<MatchLineupDto> HomeTeamLineup { get; set; }
        public List<MatchLineupDto> AwayTeamLineup { get; set; }
        public List<MatchLineupDto> HomeTeamSubstitutes { get; set; }
        public List<MatchLineupDto> AwayTeamSubstitutes { get; set; }
        public List<PredictionDto> Predictions { get; set; }
    }
} 