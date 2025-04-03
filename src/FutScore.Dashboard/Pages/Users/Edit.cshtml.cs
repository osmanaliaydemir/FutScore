using FutScore.Application.DTOs.User;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Users
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly IUserService _userService;

        public EditModel(IUserService userService)
        {
            _userService = userService;
        }

        [BindProperty]
        public UpdateUserDto User { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }

            var user = result.Data;
            User = new UpdateUserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _userService.UpdateUserAsync(id, User);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Kullanıcı başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }

            foreach (var error in result.ValidationResults)
            {
                ModelState.AddModelError(string.Empty, error.ErrorMessage);
            }

            return Page();
        }
    }
} 