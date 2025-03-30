using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using System;
using System.Collections.Generic;

namespace FutScore.Application.DTOs.Team
{
    public class TeamDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Stadium { get; set; }
        public int Founded { get; set; }
        public string Logo { get; set; }
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
        public ICollection<MatchDto> HomeMatches { get; set; }
        public ICollection<MatchDto> AwayMatches { get; set; }
        public ICollection<PlayerDto> Players { get; set; }
    }
} 