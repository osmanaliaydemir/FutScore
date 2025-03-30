using System;
using System.Collections.Generic;

namespace FutScore.Application.DTOs.User
{
    public class UserDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string ProfilePictureUrl { get; set; }
        public string Bio { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string DeletedBy { get; set; }
        public string Password { get; set; }

        // Navigation Properties
        public ICollection<UserRoleDto> UserRoles { get; set; }
        public ICollection<UserPermissionDto> UserPermissions { get; set; }
        public ICollection<UserSessionDto> UserSessions { get; set; }
        public ICollection<UserDeviceDto> UserDevices { get; set; }
        public ICollection<UserLocationDto> UserLocations { get; set; }
        public ICollection<UserLanguageDto> UserLanguages { get; set; }
        public ICollection<UserThemeDto> UserThemes { get; set; }
        public ICollection<UserPreferenceDto> UserPreferences { get; set; }
        public ICollection<UserSettingDto> UserSettings { get; set; }
        public ICollection<UserTokenDto> UserTokens { get; set; }
        public ICollection<RefreshTokenDto> RefreshTokens { get; set; }
        public ICollection<UserVerificationDto> UserVerifications { get; set; }
        public ICollection<UserResetPasswordDto> UserResetPasswords { get; set; }
        public ICollection<UserAchievementDto> UserAchievements { get; set; }
        public ICollection<UserProgressDto> UserProgresses { get; set; }
        public ICollection<UserStatisticsDto> UserStatistics { get; set; }
        public ICollection<UserAnalyticsDto> UserAnalytics { get; set; }
        public ICollection<UserReportDto> UserReports { get; set; }
        public ICollection<UserBlockDto> UserBlocks { get; set; }
        public ICollection<UserSubscriptionDto> UserSubscriptions { get; set; }
        public ICollection<UserPaymentDto> UserPayments { get; set; }
    }
} 