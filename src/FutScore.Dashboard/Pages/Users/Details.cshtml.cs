using FutScore.Application.DTOs.User;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Users
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly IUserService _userService;

        public DetailsModel(IUserService userService)
        {
            _userService = userService;
        }

        public UserDto User { get; set; } = null!;

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _userService.GetUserByIdAsync(id);
            if (!result.Success)
            {
                return NotFound();
            }

            User = result.Data;
            return Page();
        }
    }
} 