using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class NotificationTemplate : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        public string Variables { get; set; }
        public bool IsActive { get; set; }

        // Navigation Properties
        public virtual ICollection<Notification> Notifications { get; set; }
    }
} 