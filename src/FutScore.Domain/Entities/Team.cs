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
        public int StadiumId { get; set; }

        [StringLength(200)]
        public string LogoUrl { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }


        // Navigation Properties
        [ForeignKey(nameof(LeagueId))]
        public virtual League League { get; set; }
        [ForeignKey(nameof(StadiumId))]
        public virtual Stadium Stadium { get; set; }
        public virtual ICollection<Match> HomeMatches { get; set; }
        public virtual ICollection<Match> AwayMatches { get; set; }
        public virtual ICollection<Player> Players { get; set; }

        public Team()
        {
            HomeMatches = new HashSet<Match>();
            AwayMatches = new HashSet<Match>();
            Players = new HashSet<Player>();
        }
    }
} 