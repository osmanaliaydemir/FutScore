using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserLanguage : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(10)]
        public string LanguageCode { get; set; }

        [StringLength(50)]
        public string LanguageName { get; set; }

        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }

        public DateTime LastUsedAt { get; set; }

        public virtual User User { get; set; }
    }
} 