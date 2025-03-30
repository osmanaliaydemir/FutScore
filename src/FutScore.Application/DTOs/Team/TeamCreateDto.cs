using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.Team
{
    public class TeamCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        [StringLength(100)]
        public string LogoUrl { get; set; }

        [Required]
        public DateTime FoundedDate { get; set; }

        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        public string StadiumName { get; set; }

        [Required]
        public int StadiumCapacity { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public string ManagerName { get; set; }

        [Required]
        public string ManagerNationality { get; set; }

        [Required]
        public DateTime ManagerAppointedDate { get; set; }
    }
} 