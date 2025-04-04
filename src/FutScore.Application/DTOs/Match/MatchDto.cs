using FutScore.Application.DTOs.Season;
using FutScore.Application.DTOs.Stadium;
using FutScore.Application.DTOs.Team;
using FutScore.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Application.DTOs.Match
{
    public class MatchDto
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }
        public string Status { get; set; }

        public SeasonDto Season { get; private set; }
        public TeamDto HomeTeam { get; private set; }
        public TeamDto AwayTeam { get; private set; }
        public StadiumDto Stadium { get; private set; }
    }
} 