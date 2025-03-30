using AutoMapper;
using FutScore.Application.DTOs.User;
using FutScore.Domain;
using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public UserService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDetailDto> GetUserDetailAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .Include(u => u.UserPermissions)
                .Include(u => u.UserPreferences)
                .Include(u => u.UserSettings)
                .FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<UserDetailDto>(user);
        }

        public async Task<List<UserListDto>> GetUserListAsync(UserFilterDto filter)
        {
            var query = _context.Users.AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(filter.SearchTerm))
            {
                query = query.Where(u => 
                    u.Username.Contains(filter.SearchTerm) || 
                    u.Email.Contains(filter.SearchTerm));
            }

            var users = await query.ToListAsync();
            return _mapper.Map<List<UserListDto>>(users);
        }

        public async Task<UserStatisticsDto> GetUserStatisticsAsync(Guid userId)
        {
            var stats = await _context.UserStatistics
                .FirstOrDefaultAsync(s => s.UserId == userId);

            return _mapper.Map<UserStatisticsDto>(stats);
        }

        public async Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null) return false;

            // Implement password change logic here
            // Verify current password
            // Hash new password
            // Update user password

            return true;
        }

        public async Task<bool> ResetPasswordAsync(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null) return false;

            // Implement password reset logic here
            // Generate reset token
            // Send reset email
            // Save reset token

            return true;
        }

        public async Task<bool> VerifyEmailAsync(string token)
        {
            var verification = await _context.UserVerifications
                .FirstOrDefaultAsync(v => v.Token == token);

            if (verification == null) return false;

            // Implement email verification logic here
            // Verify token
            // Update user email verification status

            return true;
        }

        public async Task<bool> UpdateUserPreferencesAsync(Guid userId, UserPreferenceDto preferences)
        {
            var userPrefs = await _context.UserPreferences
                .FirstOrDefaultAsync(p => p.UserId == userId);

            if (userPrefs == null) return false;

            _mapper.Map(preferences, userPrefs);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateUserSettingsAsync(Guid userId, UserSettingDto settings)
        {
            var userSettings = await _context.UserSettings
                .FirstOrDefaultAsync(s => s.UserId == userId);

            if (userSettings == null) return false;

            _mapper.Map(settings, userSettings);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> BlockUserAsync(Guid userId, Guid blockedUserId, string reason)
        {
            //var block = new UserBlock
            //{
            //    UserId = userId,
            //    BlockedUserId = blockedUserId,
            //    BlockReason = reason,
            //    BlockedAt = DateTime.UtcNow
            //};

            //_context.UserBlocks.Add(block);
            //await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UnblockUserAsync(Guid userId, Guid blockedUserId)
        {
            //var block = await _context.UserBlocks
            //    .FirstOrDefaultAsync(b => b.UserId == userId && b.BlockedUserId == blockedUserId);

            //if (block == null) return false;

            //block.IsActive = false;
            //block.UnblockedAt = DateTime.UtcNow;
            //await _context.SaveChangesAsync();

            return true;
        }

        public async Task<List<UserAchievementDto>> GetUserAchievementsAsync(Guid userId)
        {
            var achievements = await _context.UserAchievements
                .Where(a => a.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserAchievementDto>>(achievements);
        }

        public async Task<UserProgressDto> GetUserProgressAsync(Guid userId)
        {
            var progress = await _context.UserProgresses
                .FirstOrDefaultAsync(p => p.UserId == userId);

            return _mapper.Map<UserProgressDto>(progress);
        }

        public async Task<List<UserAnalyticsDto>> GetUserAnalyticsAsync(Guid userId, DateTime startDate, DateTime endDate)
        {
            var analytics = await _context.UserAnalytics
                .Where(a => a.UserId == userId && 
                           a.AnalyticsDate >= startDate && 
                           a.AnalyticsDate <= endDate)
                .ToListAsync();

            return _mapper.Map<List<UserAnalyticsDto>>(analytics);
        }

        public async Task<List<UserReportDto>> GetUserReportsAsync(Guid userId)
        {
            var reports = await _context.UserReports
                .Where(r => r.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserReportDto>>(reports);
        }

        public async Task<List<UserSubscriptionDto>> GetUserSubscriptionsAsync(Guid userId)
        {
            var subscriptions = await _context.UserSubscriptions
                .Where(s => s.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserSubscriptionDto>>(subscriptions);
        }

        public async Task<List<UserPaymentDto>> GetUserPaymentsAsync(Guid userId)
        {
            var payments = await _context.UserPayments
                .Where(p => p.UserId == userId)
                .ToListAsync();

            return _mapper.Map<List<UserPaymentDto>>(payments);
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _context.Users
                .Include(u => u.Predictions)
                .OrderBy(u => u.Username)
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.Predictions)
                .FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<ProcessResult> AddAsync(UserDto entity)
        {
            try
            {
                var user = _mapper.Map<User>(entity);
                user.CreatedAt = DateTime.UtcNow;
                user.IsActive = true;
                //user.PasswordHash = HashPassword(entity.Password);
                user.PasswordHash = entity.Password;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();

                return new ProcessResult { Success = true, Message = "User added successfully" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = $"Error adding user: {ex.Message}" };
            }
        }

        public async Task<ProcessResult> UpdateAsync(UserDto entity)
        {
            try
            {
                var user = await _context.Users.FindAsync(entity.Id);
                if (user == null)
                    return new ProcessResult { Success = false, Message = "User not found" };

                _mapper.Map(entity, user);
                user.UpdatedAt = DateTime.UtcNow;

                //if (!string.IsNullOrEmpty(entity.Password))
                //{
                //    user.PasswordHash = HashPassword(entity.Password);
                //}

                await _context.SaveChangesAsync();
                return new ProcessResult { Success = true, Message = "User updated successfully" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = $"Error updating user: {ex.Message}" };
            }
        }

        public async Task<ProcessResult> DeleteByIdAsync(Guid id)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.Predictions)
                    .FirstOrDefaultAsync(u => u.Id == id);

                if (user == null)
                    return new ProcessResult { Success = false, Message = "User not found" };

                if (user.Predictions.Any())
                    return new ProcessResult { Success = false, Message = "Cannot delete user with associated predictions" };

                _context.Users.Remove(user);
                await _context.SaveChangesAsync();

                return new ProcessResult { Success = true, Message = "User deleted successfully" };
            }
            catch (Exception ex)
            {
                return new ProcessResult { Success = false, Message = $"Error deleting user: {ex.Message}" };
            }
        }
    }
} 