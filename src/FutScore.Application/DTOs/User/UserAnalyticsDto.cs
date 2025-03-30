using System;

namespace FutScore.Application.DTOs.User
{
    public class UserAnalyticsDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string AnalyticsType { get; set; }
        public string AnalyticsName { get; set; }
        public string AnalyticsDescription { get; set; }
        public string AnalyticsData { get; set; }
        public DateTime AnalyticsDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }

        // Navigation Properties
        public UserDto User { get; set; }
    }
} 