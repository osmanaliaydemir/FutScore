using FutScore.Application.DTOs.User;
using FutScore.Domain;

namespace FutScore.Application.Interfaces
{
    public interface IUserService
    {
        Task<ProcessResult<LoginResponseDto>> LoginAsync(LoginDto loginDto);
        Task<ProcessResult> RegisterAsync(RegisterDto registerDto);
        Task<ProcessResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto);
        Task<ProcessResult> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto);
        Task<ProcessResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto);
        Task<ProcessResult> ForgotPasswordAsync(string email);
        Task<ProcessResult<UserDto>> GetUserByIdAsync(int userId);
        Task<ProcessResult<IEnumerable<UserDto>>> GetAllUsersAsync();
        Task<ProcessResult> DeleteUserAsync(int userId);
        Task<ProcessResult> ToggleUserStatusAsync(int userId);
    }
} 