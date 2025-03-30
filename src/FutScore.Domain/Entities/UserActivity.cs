using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserActivity : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string ActivityType { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string Data { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
    }
} 