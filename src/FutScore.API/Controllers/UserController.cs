using FutScore.Application.DTOs.User;
using FutScore.Application.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetById(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        //[HttpPost("register")]
        //[AllowAnonymous]
        //public async Task<ActionResult<UserDto>> Register([FromBody] UserRegisterDto registerDto)
        //{
        //    var result = await _userService.RegisterAsync(registerDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return CreatedAtAction(nameof(GetById), new { id = result.Data.Id }, result.Data);
        //}

        //[HttpPost("login")]
        //[AllowAnonymous]
        //public async Task<ActionResult<UserLoginResponseDto>> Login([FromBody] UserLoginDto loginDto)
        //{
        //    var result = await _userService.LoginAsync(loginDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Data);
        //}

        //[HttpPut("{id}")]
        //[Authorize]
        //public async Task<ActionResult<UserDto>> Update(Guid id, [FromBody] UserUpdateDto updateDto)
        //{
        //    if (id != updateDto.Id)
        //        return BadRequest();

        //    var result = await _userService.UpdateAsync(updateDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Data);
        //}

        //[HttpDelete("{id}")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var result = await _userService.DeleteByIdAsync(id);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return NoContent();
        //}

        //[HttpPost("refresh-token")]
        //[Authorize]
        //public async Task<ActionResult<UserLoginResponseDto>> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
        //{
        //    var result = await _userService.RefreshTokenAsync(refreshTokenDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Data);
        //}

        //[HttpPost("logout")]
        //[Authorize]
        //public async Task<IActionResult> Logout()
        //{
        //    var result = await _userService.LogoutAsync();
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return NoContent();
        //}

        //[HttpPost("verify-email")]
        //[AllowAnonymous]
        //public async Task<IActionResult> VerifyEmail([FromBody] EmailVerificationDto verificationDto)
        //{
        //    var result = await _userService.VerifyEmailAsync(verificationDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpPost("forgot-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        //{
        //    var result = await _userService.ForgotPasswordAsync(forgotPasswordDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}

        //[HttpPost("reset-password")]
        //[AllowAnonymous]
        //public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        //{
        //    var result = await _userService.ResetPasswordAsync(resetPasswordDto);
        //    if (!result.Success)
        //        return BadRequest(result.Message);

        //    return Ok(result.Message);
        //}
    }
} 