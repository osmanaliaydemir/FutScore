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
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string City { get; set; }

        [Required]
        public int Capacity { get; set; }

        [StringLength(200)]
        public string? ImageUrl { get; set; }

        // Navigation Properties
        public virtual ICollection<Match> Matches { get; set; }

        public Stadium()
        {
            Matches = new HashSet<Match>();
        }
    }
} 