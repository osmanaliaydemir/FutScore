using System;
using System.Collections.Generic;
using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.DTOs.Prediction;
using FutScore.Application.DTOs.Team;

namespace FutScore.Application.DTOs.Match
{
    public class MatchDto
    {
        public Guid Id { get; set; }
        public Guid HomeTeamId { get; set; }
        public Guid AwayTeamId { get; set; }
        public Guid LeagueId { get; set; }
        public DateTime MatchDate { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public string LeagueName { get; set; }
        public string Status { get; set; }
        public string Score { get; set; }
        public string Venue { get; set; }
        public string Referee { get; set; }
        public string Attendance { get; set; }
        public string Weather { get; set; }
        public string Temperature { get; set; }
        public string AdditionalInfo { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public TeamDto HomeTeam { get; set; }
        public TeamDto AwayTeam { get; set; }
        public LeagueDto League { get; set; }
        public ICollection<MatchEventDto> Events { get; set; }
        public ICollection<PredictionDto> Predictions { get; set; }
    }
} 