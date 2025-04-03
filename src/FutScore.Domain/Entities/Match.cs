using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public enum MatchStatus
    {
        Scheduled,
        Live,
        Completed,
        Postponed,
        Cancelled
    }

    public class Match : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SeasonId { get; set; }

        [Required]
        public int HomeTeamId { get; set; }

        [Required]
        public int AwayTeamId { get; set; }

        [Required]
        public int StadiumId { get; set; }

        [Required]
        public DateTime MatchDate { get; set; }

        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }

        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }

        [Required]
        public MatchStatus Status { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(SeasonId))]
        public virtual Season Season { get; set; }

        [ForeignKey(nameof(HomeTeamId))]
        public virtual Team HomeTeam { get; set; }

        [ForeignKey(nameof(AwayTeamId))]
        public virtual Team AwayTeam { get; set; }

        [ForeignKey(nameof(StadiumId))]
        public virtual Stadium Stadium { get; set; }

        public Match()
        {
            Status = MatchStatus.Scheduled;
        }
    }
} 