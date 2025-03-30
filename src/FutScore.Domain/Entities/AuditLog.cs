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

        public string EntityId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }

        public string IpAddress { get; set; }
        public string UserAgent { get; set; }

        public string AdditionalData { get; set; }

        public string RequestPath { get; set; }
        public string RequestMethod { get; set; }
        public string RequestParameters { get; set; }

        public string ResponseStatusCode { get; set; }
        public string ResponseBody { get; set; }

        public string Exception { get; set; }
        public string StackTrace { get; set; }

        public virtual User User { get; set; }
    }
} 