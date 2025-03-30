using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class PlayerSeason : BaseEntity
    {
        [Required]
        public Guid PlayerId { get; set; }

        [Required]
        public Guid TeamId { get; set; }

        [Required]
        [StringLength(20)]
        public string Season { get; set; }

        public int Appearances { get; set; }
        public int Goals { get; set; }
        public int Assists { get; set; }
        public int YellowCards { get; set; }
        public int RedCards { get; set; }
        public int MinutesPlayed { get; set; }

        public int CleanSheets { get; set; }
        public int GoalsConceded { get; set; }
        public int Saves { get; set; }
        public int PenaltiesSaved { get; set; }

        public int ManOfTheMatch { get; set; }
        public double Rating { get; set; }

        public string Position { get; set; }
        public string JerseyNumber { get; set; }
        public bool IsActive { get; set; }

        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }

        public virtual Player Player { get; set; }
        public virtual Team Team { get; set; }
    }
} 