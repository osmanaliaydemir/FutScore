using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class SeasonTeam : BaseEntity
    {
        [Required]
        public int SeasonId { get; set; }

        [Required]
        public int TeamId { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Season { get; set; }

        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }
    }
} 