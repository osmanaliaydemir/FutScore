using System;

namespace FutScore.Application.DTOs.User
{
    public class UserSessionDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string SessionToken { get; set; }
        public string DeviceInfo { get; set; }
        public string IpAddress { get; set; }
        public string UserAgent { get; set; }
        public DateTime LastActivity { get; set; }
        public DateTime ExpiresAt { get; set; }
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