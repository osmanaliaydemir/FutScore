using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.League
{
    public class LeagueUpdateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [StringLength(100)]
        public string LogoUrl { get; set; }

        [StringLength(100)]
        public string BannerUrl { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool IsActive { get; set; }
        public string MatchStatus { get; set; }
    }
} 