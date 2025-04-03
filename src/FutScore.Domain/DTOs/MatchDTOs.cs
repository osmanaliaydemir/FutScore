using System;

namespace FutScore.Domain.DTOs
{
    public class MatchDTO
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int StadiumId { get; set; }
        public DateTime MatchDate { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public string Status { get; set; }
    }

    public class MatchDetailDTO : MatchDTO
    {
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string StadiumName { get; set; }
        public string LeagueName { get; set; }
        public string SeasonName { get; set; }
    }

    public class CreateMatchDTO
    {
        public int SeasonId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int StadiumId { get; set; }
        public DateTime MatchDate { get; set; }
    }

    public class UpdateMatchDTO
    {
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public string Status { get; set; }
    }
} 