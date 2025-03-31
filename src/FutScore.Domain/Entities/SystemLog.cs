using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class SystemLog : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string LogLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        [StringLength(4000)]
        public string Message { get; set; }

        [StringLength(4000)]
        public string Exception { get; set; }
        [StringLength(4000)]
        public string StackTrace { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(4000)]
        public string AdditionalData { get; set; }
    }
} 