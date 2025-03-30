using System;

namespace FutScore.Application.DTOs.User
{
    public class UserLocationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Region { get; set; }
        public string TimeZone { get; set; }
        public string IpAddress { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime LastUpdated { get; set; }
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