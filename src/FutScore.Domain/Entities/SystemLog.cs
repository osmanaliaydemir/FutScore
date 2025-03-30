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
        public string Message { get; set; }

        public string Exception { get; set; }
        public string StackTrace { get; set; }

        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public string AdditionalData { get; set; }
    }
} 