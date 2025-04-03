using AutoMapper;
using BCrypt.Net;
using FutScore.Application.DTOs.User;
using FutScore.Application.Interfaces;
using FutScore.Application.Settings;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FutScore.Application.Services
{
    public class UserService : IUserService, IBaseService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        public UserService(IUserRepository userRepository,IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }
        public async Task<ProcessResult<LoginResponseDto>> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetByUsernameAsync(loginDto.Username);

            if (user == null || !user.IsActive)
            {
                return new ProcessResult<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                return new ProcessResult<LoginResponseDto>
                {
                    Success = false,
                    Message = "Invalid username or password"
                };
            }

            await _userRepository.UpdateLastLoginDateAsync(user.Id);

            var token = GenerateJwtToken(user);
            var refreshToken = GenerateRefreshToken();

            // Save refresh token
            var userRefreshToken = new UserRefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            user.RefreshTokens.Add(userRefreshToken);
            await _userRepository.UpdateAsync(user);

            var response = new LoginResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Role = user.Role,
                Token = token,
                RefreshToken = refreshToken
            };

            return new ProcessResult<LoginResponseDto>
            {
                Success = true,
                Data = response
            };
        }

        public async Task<ProcessResult> RegisterAsync(RegisterDto registerDto)
        {
            if (!await _userRepository.IsUsernameUniqueAsync(registerDto.Username))
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Username is already taken"
                };
            }

            if (!await _userRepository.IsEmailUniqueAsync(registerDto.Email))
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Email is already taken"
                };
            }

            var user = _mapper.Map<User>(registerDto);
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);

            return await _userRepository.AddAsync(user);
        }

        public async Task<ProcessResult> UpdateUserAsync(int userId, UpdateUserDto updateUserDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            if (!await _userRepository.IsEmailUniqueAsync(updateUserDto.Email, userId))
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Email is already taken"
                };
            }

            _mapper.Map(updateUserDto, user);
            user.UpdatedDate = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<ProcessResult> ChangePasswordAsync(int userId, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            if (!BCrypt.Net.BCrypt.Verify(changePasswordDto.CurrentPassword, user.PasswordHash))
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Current password is incorrect"
                };
            }

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            user.UpdatedDate = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        public async Task<ProcessResult> ResetPasswordAsync(ResetPasswordDto resetPasswordDto)
        {
            // Implement password reset logic
            return new ProcessResult
            {
                Success = false,
                Message = "Not implemented"
            };
        }

        public async Task<ProcessResult> ForgotPasswordAsync(string email)
        {
            // Implement forgot password logic
            return new ProcessResult
            {
                Success = false,
                Message = "Not implemented"
            };
        }

        public async Task<ProcessResult<UserDto>> GetUserByIdAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ProcessResult<UserDto>
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            var userDto = _mapper.Map<UserDto>(user);
            return new ProcessResult<UserDto>
            {
                Success = true,
                Data = userDto
            };
        }

        public async Task<ProcessResult<IEnumerable<UserDto>>> GetAllUsersAsync()
        {
            var users = await _userRepository.GetAllAsync();
            var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

            return new ProcessResult<IEnumerable<UserDto>>
            {
                Success = true,
                Data = userDtos
            };
        }

        public async Task<ProcessResult> DeleteUserAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            return await _userRepository.DeleteAsync(user);
        }

        public async Task<ProcessResult> ToggleUserStatusAsync(int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "User not found"
                };
            }

            user.IsActive = !user.IsActive;
            user.UpdatedDate = DateTime.UtcNow;

            return await _userRepository.UpdateAsync(user);
        }

        private string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationInMinutes),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience,
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
} 