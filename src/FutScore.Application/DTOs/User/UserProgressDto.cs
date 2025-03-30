using System;

namespace FutScore.Application.DTOs.User
{
    public class UserProgressDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string ProgressType { get; set; }
        public string ProgressName { get; set; }
        public string ProgressDescription { get; set; }
        public int CurrentValue { get; set; }
        public int TargetValue { get; set; }
        public double Percentage { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? CompletedAt { get; set; }
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