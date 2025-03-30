using System;

namespace FutScore.Application.DTOs.User
{
    public class UserAchievementDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string AchievementName { get; set; }
        public string AchievementDescription { get; set; }
        public string AchievementType { get; set; }
        public int Points { get; set; }
        public bool IsUnlocked { get; set; }
        public DateTime? UnlockedAt { get; set; }
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