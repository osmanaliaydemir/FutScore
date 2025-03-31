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
        [StringLength(1000)]
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
        [StringLength(100)]
        public string LastModifiedBy { get; set; }

        public DateTime? ExpiresAt { get; set; }

        [StringLength(1000)]
        public string DefaultValue { get; set; }
        [StringLength(1000)]
        public string ValidationRules { get; set; }
        [StringLength(1000)]
        public string AllowedValues { get; set; }

        [StringLength(50)]
        public string Environment { get; set; }
        [StringLength(50)]
        public string Application { get; set; }
        [StringLength(50)]
        public string Module { get; set; }
    }
} 