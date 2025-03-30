using FutScore.Application.DTOs.User;
using FutScore.Application.Services.RoleService;
using FutScore.Domain;
using FutScore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FutScore.Application.Services.AdminUserService
{
    public class AdminUserService : IAdminUserService
    {
        private readonly AppDbContext _context;
        private readonly IRoleService _roleService;

        public AdminUserService(AppDbContext context, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        public async Task<UserDto> CreateAdminUserAsync(UserCreateDto userDto)
        {
            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == userDto.Username))
            {
                throw new InvalidOperationException("Bu kullanıcı adı zaten kullanılıyor.");
            }

            // Create new user
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = HashPassword(userDto.Password),
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Assign admin role
            await _roleService.AssignRolesToUserAsync(user.Id, new List<string> { "Admin" });

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt
            };
        }

        public async Task<UserDto> GetAdminUserByIdAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLoginDate = user.LastLoginDate
            };
        }

        public async Task<UserDto> GetAdminUserByUsernameAsync(string username)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                return null;

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLoginDate = user.LastLoginDate
            };
        }

        public async Task<List<UserDto>> GetAllAdminUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginDate = u.LastLoginDate
                })
                .ToListAsync();
        }

        public async Task<UserDto> UpdateAdminUserAsync(UserUpdateDto userDto)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userDto.Id && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                throw new KeyNotFoundException($"Admin kullanıcısı bulunamadı: {userDto.Id}");

            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsActive = user.IsActive,
                CreatedAt = user.CreatedAt,
                LastLoginDate = user.LastLoginDate
            };
        }

        public async Task DeleteAdminUserAsync(Guid id)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                throw new KeyNotFoundException($"Admin kullanıcısı bulunamadı: {id}");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsAdminUserAsync(Guid userId)
        {
            return await _context.UserRoles
                .Include(ur => ur.Role)
                .AnyAsync(ur => ur.UserId == userId && ur.Role.Name == "Admin");
        }

        public async Task<bool> IsAdminUserAsync(string username)
        {
            return await _context.UserRoles
                .Include(ur => ur.User)
                .Include(ur => ur.Role)
                .AnyAsync(ur => ur.User.Username == username && ur.Role.Name == "Admin");
        }

        public async Task<bool> ValidateAdminCredentialsAsync(string username, string password)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user == null || !user.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                return false;

            return VerifyPassword(password, user.PasswordHash);
        }

        public async Task ResetAdminPasswordAsync(Guid userId, string newPassword)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                throw new KeyNotFoundException($"Admin kullanıcısı bulunamadı: {userId}");

            user.PasswordHash = HashPassword(newPassword);
            await _context.SaveChangesAsync();
        }

        public async Task DeactivateAdminUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                throw new KeyNotFoundException($"Admin kullanıcısı bulunamadı: {userId}");

            user.IsActive = false;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task ActivateAdminUserAsync(Guid userId)
        {
            var user = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == userId && u.UserRoles.Any(ur => ur.Role.Name == "Admin"));

            if (user == null)
                throw new KeyNotFoundException($"Admin kullanıcısı bulunamadı: {userId}");

            user.IsActive = true;
            user.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetInactiveAdminUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => !u.IsActive && u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginDate = u.LastLoginDate
                })
                .ToListAsync();
        }

        public async Task<List<UserDto>> GetActiveAdminUsersAsync()
        {
            return await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .Where(u => u.IsActive && u.UserRoles.Any(ur => ur.Role.Name == "Admin"))
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Username = u.Username,
                    Email = u.Email,
                    FirstName = u.FirstName,
                    LastName = u.LastName,
                    IsActive = u.IsActive,
                    CreatedAt = u.CreatedAt,
                    LastLoginDate = u.LastLoginDate
                })
                .ToListAsync();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
} 