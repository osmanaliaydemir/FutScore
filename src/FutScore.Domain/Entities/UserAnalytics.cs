using System;
using System.ComponentModel.DataAnnotations;

namespace FutScore.Domain.Entities
{
    public class UserAnalytics : BaseEntity
    {
        [Required]
        public Guid UserId { get; set; }

        public int TotalSessions { get; set; }
        public int TotalTimeSpent { get; set; }
        public int AverageSessionDuration { get; set; }

        public int TotalPageViews { get; set; }
        public int UniquePageViews { get; set; }
        public string? MostVisitedPages { get; set; }

        public int TotalInteractions { get; set; }
        public int TotalClicks { get; set; }
        public int TotalScrolls { get; set; }

        public string? DeviceTypes { get; set; }
        public string? BrowserTypes { get; set; }
        public string? OperatingSystems { get; set; }

        public DateTime? AnalyticsDate { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public string? ActivityPatterns { get; set; }

        public virtual User? User { get; set; }
    }
} 