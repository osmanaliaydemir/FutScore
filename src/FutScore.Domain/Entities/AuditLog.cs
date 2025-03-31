using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class AuditLog : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Action { get; set; }

        [Required]
        [StringLength(100)]
        public string EntityName { get; set; }

        [StringLength(100)]
        public string EntityId { get; set; }
        [StringLength(4000)]
        public string OldValues { get; set; }
        [StringLength(4000)]
        public string NewValues { get; set; }

        [StringLength(50)]
        public string IpAddress { get; set; }
        [StringLength(500)]
        public string UserAgent { get; set; }

        [StringLength(4000)]
        public string AdditionalData { get; set; }

        [StringLength(500)]
        public string RequestPath { get; set; }
        [StringLength(10)]
        public string RequestMethod { get; set; }
        [StringLength(4000)]
        public string RequestParameters { get; set; }

        [StringLength(10)]
        public string ResponseStatusCode { get; set; }
        [StringLength(4000)]
        public string ResponseBody { get; set; }

        [StringLength(4000)]
        public string Exception { get; set; }
        [StringLength(4000)]
        public string StackTrace { get; set; }

        public virtual User User { get; set; }
    }
} 