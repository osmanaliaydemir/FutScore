using AutoMapper;
using BCrypt.Net;
using FutScore.Application.DTOs.User;
using FutScore.Application.Services.RoleService;
using FutScore.Domain;
using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        private readonly IRoleService _roleService;

        public UserService(AppDbContext context, IMapper mapper, IRoleService roleService)
        {
            _context = context;
            _mapper = mapper;
            _roleService = roleService;
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
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<UserDto> GetByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> CreateAsync(UserCreateDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            user.PasswordHash = HashPassword(userDto.Password);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            if (userDto.Roles != null)
            {
                foreach (var roleId in userDto.Roles)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId = Guid.Parse(roleId),
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.UserRoles.Add(userRole);
                }
                await _context.SaveChangesAsync();
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserUpdateDto userDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .FirstOrDefaultAsync(u => u.Id == userDto.Id);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            _mapper.Map(userDto, user);
            user.UpdatedAt = DateTime.UtcNow;

            if (userDto.Roles != null)
            {
                _context.UserRoles.RemoveRange(user.UserRoles);
                foreach (var roleId in userDto.Roles)
                {
                    var userRole = new UserRole
                    {
                        UserId = user.Id,
                        RoleId =Guid.Parse(roleId),
                        CreatedAt = DateTime.UtcNow
                    };
                    _context.UserRoles.Add(userRole);
                }
            }

            await _context.SaveChangesAsync();
            return _mapper.Map<UserDto>(user);
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null)
                return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task ResetPasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            user.PasswordHash = HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsActiveAsync(Guid userId)
        {
            var user = await _context.Users.FindAsync(userId);
            return user?.IsActive ?? false;
        }

        public async Task<bool> IsActiveAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
            return user?.IsActive ?? false;
        }

        public async Task<bool> IsAdminAsync(Guid userId)
        {
            return await IsAdminUserAsync(userId);
        }

        public async Task<bool> IsAdminAsync(string username)
        {
            return await IsAdminUserAsync(username);
        }

        public async Task<bool> IsAdminUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user?.UserRoles.Any(ur => ur.Role.Name == "Admin") ?? false;
        }

        public async Task<bool> IsAdminUserAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            return user?.UserRoles.Any(ur => ur.Role.Name == "Admin") ?? false;
        }

        public async Task<bool> ValidateAdminCredentialsAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || !user.IsActive)
                return false;

            if (!user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task ResetAdminPasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            if (!user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                throw new Exception("Bu kullanıcı admin değil.");

            user.PasswordHash = HashPassword(newPassword);
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task ActivateAdminUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            if (!user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                throw new Exception("Bu kullanıcı admin değil.");

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAdminUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
                throw new Exception("Kullanıcı bulunamadı.");

            if (!user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                throw new Exception("Bu kullanıcı admin değil.");

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetInactiveAdminUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => !u.IsActive && u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task<List<UserDto>> GetActiveAdminUsersAsync()
        {
            var users = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.IsActive && u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                .ToListAsync();

            return _mapper.Map<List<UserDto>>(users);
        }

        private string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
} 