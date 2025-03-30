using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserReport : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid ReportedId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReportType { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public string Evidence { get; set; }
        public string Status { get; set; }
        public string Resolution { get; set; }

        public DateTime? ResolvedAt { get; set; }
        public string ResolvedBy { get; set; }

        public virtual User Reporter { get; set; }
        public virtual User Reported { get; set; }
    }
} 