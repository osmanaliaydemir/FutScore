using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FutScore.Domain.Entities
{
    public class Player : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TeamId { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string? Position { get; set; }

        public int? JerseyNumber { get; set; }
        public DateTime? DateOfBirth { get; set; }

        // Navigation Properties
        [ForeignKey(nameof(TeamId))]
        public virtual Team Team { get; set; }
    }
} 