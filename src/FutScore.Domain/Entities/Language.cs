using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Language : BaseEntity
    {
        [Required]
        [StringLength(10)]
        public string Code { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string NativeName { get; set; }

        public bool IsActive { get; set; }
        public bool IsDefault { get; set; }

        // Navigation Properties
        public virtual ICollection<Localization> Localizations { get; set; }
    }
} 