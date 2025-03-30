using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class Configuration : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [StringLength(50)]
        public string Group { get; set; }

        [StringLength(50)]
        public string DataType { get; set; }

        public bool IsActive { get; set; }
        public bool IsEncrypted { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public string DefaultValue { get; set; }
        public string ValidationRules { get; set; }
        public string AllowedValues { get; set; }

        public string Environment { get; set; }
        public string Application { get; set; }
        public string Module { get; set; }
    }
} 