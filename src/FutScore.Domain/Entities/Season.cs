using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public enum SeasonStatus
    {
        Upcoming,
        Active,
        Completed,
        Cancelled
    }

    public class Season : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int LeagueId { get; set; }

        [Required]
        [StringLength(50)]
        public string SeasonName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [Required]
        public SeasonStatus Status { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(LeagueId))]
        public virtual League League { get; set; }
        public virtual ICollection<Match> Matches { get; set; }

        public Season()
        {
            Matches = new HashSet<Match>();
            Status = SeasonStatus.Upcoming;
        }
    }
} 