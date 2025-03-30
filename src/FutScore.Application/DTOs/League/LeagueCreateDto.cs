using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.League
{
    public class LeagueCreateDto
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

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public int MaxTeams { get; set; }

        [Required]
        public bool IsActive { get; set; }
    }
} 