using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class SystemSettings : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public string Group { get; set; }
        public string DataType { get; set; }

        public bool IsSystemSetting { get; set; }
        public bool IsEncrypted { get; set; }

        public DateTime? LastModifiedAt { get; set; }
        public string LastModifiedBy { get; set; }
    }
} 