using FutScore.Application.DTOs.User;

namespace FutScore.Application.Services.AdminUserService
{
    public interface IAdminUserService
    {
        Task<UserDto> CreateAdminUserAsync(UserCreateDto userDto);
        Task<UserDto> GetAdminUserByIdAsync(Guid id);
        Task<UserDto> GetAdminUserByUsernameAsync(string username);
        Task<List<UserDto>> GetAllAdminUsersAsync();
        Task<UserDto> UpdateAdminUserAsync(UserUpdateDto userDto);
        Task DeleteAdminUserAsync(Guid id);
        Task<bool> IsAdminUserAsync(Guid userId);
        Task<bool> IsAdminUserAsync(string username);
        Task<bool> ValidateAdminCredentialsAsync(string username, string password);
        Task ResetAdminPasswordAsync(Guid userId, string newPassword);
        Task DeactivateAdminUserAsync(Guid userId);
        Task ActivateAdminUserAsync(Guid userId);
        Task<List<UserDto>> GetInactiveAdminUsersAsync();
        Task<List<UserDto>> GetActiveAdminUsersAsync();
    }
} 