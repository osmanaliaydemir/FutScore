using FutScore.Application.DTOs.User;
using FutScore.Application.Services.GenericService;
using FutScore.Domain.Entities;

namespace FutScore.Application.Services.UserService
{
    public interface IUserService
    {
        Task<UserDetailDto> GetUserDetailAsync(Guid id);
        Task<List<UserListDto>> GetUserListAsync(UserFilterDto filter);
        Task<UserStatisticsDto> GetUserStatisticsAsync(Guid userId);
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);
        Task<bool> ResetPasswordAsync(string email);
        Task<bool> VerifyEmailAsync(string token);
        Task<bool> UpdateUserPreferencesAsync(Guid userId, UserPreferenceDto preferences);
        Task<bool> UpdateUserSettingsAsync(Guid userId, UserSettingDto settings);
        Task<bool> BlockUserAsync(Guid userId, Guid blockedUserId, string reason);
        Task<bool> UnblockUserAsync(Guid userId, Guid blockedUserId);
        Task<List<UserAchievementDto>> GetUserAchievementsAsync(Guid userId);
        Task<UserProgressDto> GetUserProgressAsync(Guid userId);
        Task<List<UserAnalyticsDto>> GetUserAnalyticsAsync(Guid userId, DateTime startDate, DateTime endDate);
        Task<List<UserReportDto>> GetUserReportsAsync(Guid userId);
        Task<List<UserSubscriptionDto>> GetUserSubscriptionsAsync(Guid userId);
        Task<List<UserPaymentDto>> GetUserPaymentsAsync(Guid userId);
        Task<UserDto> GetByIdAsync(Guid id);
        Task<UserDto> GetByUsernameAsync(string username);
        Task<List<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(UserCreateDto userDto);
        Task<UserDto> UpdateAsync(UserUpdateDto userDto);
        Task DeleteAsync(Guid id);
        Task<bool> ValidateCredentialsAsync(string username, string password);
        Task ResetPasswordAsync(Guid userId, string newPassword);
        Task<bool> IsActiveAsync(Guid userId);
        Task<bool> IsActiveAsync(string username);
        Task<bool> IsAdminAsync(Guid userId);
        Task<bool> IsAdminAsync(string username);
        Task<bool> IsAdminUserAsync(Guid userId);
        Task<bool> IsAdminUserAsync(string username);
        Task<bool> ValidateAdminCredentialsAsync(string username, string password);
        Task ResetAdminPasswordAsync(Guid userId, string newPassword);
        Task ActivateAdminUserAsync(Guid userId);
        Task DeactivateAdminUserAsync(Guid userId);
        Task<List<UserDto>> GetInactiveAdminUsersAsync();
        Task<List<UserDto>> GetActiveAdminUsersAsync();
    }
} 