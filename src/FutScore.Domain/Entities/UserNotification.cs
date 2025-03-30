using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserNotification : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        [Required]
        public Guid NotificationId { get; set; }

        public bool IsRead { get; set; }
        public DateTime? ReadAt { get; set; }

        // Navigation Properties
        public virtual User User { get; set; }
        public virtual Notification Notification { get; set; }
    }
} 