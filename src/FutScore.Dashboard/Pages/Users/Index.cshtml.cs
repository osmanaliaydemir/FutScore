using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FutScore.Application.DTOs.User;
using FutScore.Application.Services.UserService;
using FutScore.Application.Services.RoleService;
using FutScore.Application.DTOs.Role;

namespace FutScore.Dashboard.Pages.Users
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public IndexModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        public List<UserDto> Users { get; set; }
        public List<RoleDto> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Users = await _userService.GetAllAsync();
            Roles = await _roleService.GetAllAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] UserCreateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.CreateAsync(user);
            return new JsonResult(new { success = true, message = "Kullanıcı başarıyla oluşturuldu." });
        }

        public async Task<IActionResult> OnPostEditAsync([FromForm] UserUpdateDto user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userService.UpdateAsync(user);
            return new JsonResult(new { success = true, message = "Kullanıcı başarıyla güncellendi." });
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _userService.DeleteAsync(id);
            return new JsonResult(new { success = true, message = "Kullanıcı başarıyla silindi." });
        }

        public async Task<IActionResult> OnPostResetPasswordAsync([FromForm] ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (model.NewPassword != model.ConfirmPassword)
            {
                return BadRequest(new { success = false, message = "Şifreler eşleşmiyor." });
            }

            await _userService.ResetPasswordAsync(model.Id, model.NewPassword);
            return new JsonResult(new { success = true, message = "Şifre başarıyla sıfırlandı." });
        }
    }
} 