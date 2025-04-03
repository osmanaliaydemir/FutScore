using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class Stadium : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public required string Name { get; set; }
        [Required]
        [StringLength(100)]
        public required string City { get; set; }
        [Required]
        public required int Capacity { get; set; }
        [StringLength(200)]
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public virtual ICollection<Team> Teams { get; set; }
        public virtual ICollection<Match> Matches { get; set; }

        public Stadium()
        {
            Teams = new HashSet<Team>();
            Matches = new HashSet<Match>();
        }
    }
} 