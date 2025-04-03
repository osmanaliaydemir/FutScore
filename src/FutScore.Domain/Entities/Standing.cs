using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class Standing : BaseEntity
    {
        [Required]
        public int SeasonId { get; set; }

        [Required]
        public int TeamId { get; set; }

        public int Points { get; set; }
        public int Wins { get; set; }
        public int Draws { get; set; }
        public int Losses { get; set; }
        public int GoalsFor { get; set; }
        public int GoalsAgainst { get; set; }

        [NotMapped]
        public int GoalDifference => GoalsFor - GoalsAgainst;

        // Navigation Properties
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Season { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }
    }
} 