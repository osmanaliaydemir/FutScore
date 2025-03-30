using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserSettings : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        public string? Theme { get; set; }
        public string? Language { get; set; }
        public string? TimeZone { get; set; }

        public bool EmailNotifications { get; set; }
        public bool PushNotifications { get; set; }
        public bool SMSNotifications { get; set; }

        public bool ShowOnlineStatus { get; set; }
        public bool ShowLastSeen { get; set; }

        public string? NotificationPreferences { get; set; }

        // Navigation Properties
        public virtual User? User { get; set; }
    }
} 