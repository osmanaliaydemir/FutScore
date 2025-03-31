using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserPreference : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        [StringLength(1000)]
        public string Value { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        public virtual User User { get; set; }
    }
} 