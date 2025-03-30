using System;

namespace FutScore.Application.DTOs.User
{
    public class UserLanguageDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string LanguageCode { get; set; }
        public string LanguageName { get; set; }
        public bool IsDefault { get; set; }
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