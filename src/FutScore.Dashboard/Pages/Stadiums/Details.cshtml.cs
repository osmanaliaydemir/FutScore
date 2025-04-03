using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Stadiums
{
    public class DetailsModel : PageModel
    {
        private readonly IStadiumService _stadiumService;

        public DetailsModel(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        public StadiumDto Stadium { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Stadium = await _stadiumService.GetStadiumByIdAsync(id);

            if (Stadium == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
} 