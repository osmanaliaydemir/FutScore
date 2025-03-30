using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.DTOs.Team;
using System;
using System.Collections.Generic;

namespace FutScore.Application.DTOs.Player
{
    public class PlayerDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string TeamName { get; set; }
        public string ShortName { get; set; }
        public string PhotoUrl { get; set; }
        public string Position { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Nationality { get; set; }
        public string JerseyNumber { get; set; }
        public string Foot { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public TeamDto Team { get; set; }
        public ICollection<MatchEventDto> MatchEvents { get; set; }
    }
} 