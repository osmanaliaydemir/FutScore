using System;


namespace FutScore.Application.DTOs.User
{
    public class UserBlockDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BlockedUserId { get; set; }
        public string BlockReason { get; set; }
        public DateTime BlockedAt { get; set; }
        public DateTime? UnblockedAt { get; set; }
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
        public UserDto BlockedUser { get; set; }
    }
} 