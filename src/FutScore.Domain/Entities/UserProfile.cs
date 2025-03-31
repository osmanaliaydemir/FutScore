using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserProfile : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [StringLength(50)]
        public string? FirstName { get; set; }

        [StringLength(50)]
        public string? LastName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        [StringLength(20)]
        public string? Gender { get; set; }

        [StringLength(100)]
        public string? Location { get; set; }

        [StringLength(500)]
        public string? Bio { get; set; }

        [StringLength(500)]
        public string? ProfilePictureUrl { get; set; }

        [StringLength(500)]
        public string? CoverPictureUrl { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 