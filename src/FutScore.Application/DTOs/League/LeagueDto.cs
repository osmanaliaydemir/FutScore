using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Season;
using FutScore.Application.DTOs.Team;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FutScore.Application.DTOs.League
{
    public class LeagueDto
    {
        public Guid Id { get; set; }
        public int TeamCount { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string LogoUrl { get; set; }
        public string BannerUrl { get; set; }
        public bool IsActive { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public ICollection<SeasonDto> Seasons { get; set; }
        public ICollection<TeamDto> Teams { get; set; }
        public ICollection<MatchDto> Matches { get; set; }
    }
}
