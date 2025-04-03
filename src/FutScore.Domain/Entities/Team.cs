using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class Team : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int LeagueId { get; set; }

        [Required]
        [StringLength(200)]
        public string LogoUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        [StringLength(100)]
        public string Stadium { get; set; }

        [Required]
        public int Founded { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(LeagueId))]
        public virtual League League { get; set; }
        public virtual ICollection<SeasonTeam> SeasonTeams { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            SeasonTeams = new HashSet<SeasonTeam>();
            HomeMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
            Players = new HashSet<Player>();
        }
    }
} 