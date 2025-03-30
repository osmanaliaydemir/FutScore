using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Localization : BaseEntity
    {
        [Required]
        public Guid LanguageId { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        public string Group { get; set; }
        public string Description { get; set; }

        // Navigation Properties
        public virtual Language Language { get; set; }
    }
} 