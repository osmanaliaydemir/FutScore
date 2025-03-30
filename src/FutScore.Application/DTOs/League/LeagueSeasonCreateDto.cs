using System.ComponentModel.DataAnnotations;

namespace FutScore.Application.DTOs.League
{
    public class LeagueSeasonCreateDto
    {
        [Required]
        public Guid LeagueId { get; set; }

        [Required]
        [StringLength(50)]
        public string SeasonName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

        [Required]
        public int MaxTeams { get; set; }

        [Required]
        public int PromotionSpots { get; set; } // Üst lige yükselme hakkı olan takım sayısı

        [Required]
        public int RelegationSpots { get; set; } // Alt lige düşme hakkı olan takım sayısı

        [Required]
        public int PlayoffSpots { get; set; } // Playoff'a katılacak takım sayısı

        [Required]
        public bool HasPlayoffs { get; set; }

        [Required]
        public bool HasRelegation { get; set; }

        [Required]
        public bool HasPromotion { get; set; }

        [Required]
        public int PointsForWin { get; set; }

        [Required]
        public int PointsForDraw { get; set; }

        [Required]
        public int PointsForLoss { get; set; }
    }
} 