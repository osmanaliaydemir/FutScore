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
        public string? Gender { get; set; }
        public string? Location { get; set; }
        public string? Bio { get; set; }

        public string? ProfilePictureUrl { get; set; }
        public string? CoverPictureUrl { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 