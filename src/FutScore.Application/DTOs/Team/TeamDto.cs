using FutScore.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Stadium;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;

namespace FutScore.Application.DTOs.Team
{
    public class TeamDto : BaseDto
    {
        public required string Name { get; set; }
        public required int LeagueId { get; set; }
        public required int StadiumId { get; set; }
        public string? LogoUrl { get; set; }
        public required string City { get; set; }
        // Navigation Properties
        [ForeignKey(nameof(LeagueId))]
        public virtual LeagueDto? League { get; set; }
        [ForeignKey(nameof(StadiumId))]
        public virtual StadiumDto? Stadium { get; set; }
        public virtual ICollection<MatchDto>? HomeMatches { get; set; }
        public virtual ICollection<MatchDto>? AwayMatches { get; set; }
        public virtual ICollection<PlayerDto>? Players { get; set; }
    }
} 