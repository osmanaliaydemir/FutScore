using FutScore.Application.DTOs.User;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Users
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly IUserService _userService;

        public IndexModel(IUserService userService)
        {
            _userService = userService;
        }

        public List<UserDto> Users { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _userService.GetAllUsersAsync();
            if (result.Success)
            {
                Users = result.Data.ToList();
            }
        }
    }
} 