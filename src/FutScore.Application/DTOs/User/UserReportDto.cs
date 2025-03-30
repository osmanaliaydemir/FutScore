using System;

namespace FutScore.Application.DTOs.User
{
    public class UserReportDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ReportType { get; set; }
        public string ReportTitle { get; set; }
        public string ReportDescription { get; set; }
        public string ReportData { get; set; }
        public DateTime ReportDate { get; set; }
        public bool IsActive { get; set; }
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