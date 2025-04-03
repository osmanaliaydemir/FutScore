using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Stadiums
{
    public class EditModel : PageModel
    {
        private readonly IStadiumService _stadiumService;

        public EditModel(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        [BindProperty]
        public UpdateStadiumDto Stadium { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var stadium = await _stadiumService.GetStadiumByIdAsync(id);
            
            if (stadium == null)
            {
                return NotFound();
            }

            Stadium = new UpdateStadiumDto
            {
                Id = stadium.Id,
                Name = stadium.Name,
                City = stadium.City,
                Capacity = stadium.Capacity
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _stadiumService.UpdateStadiumAsync(Stadium);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Stadyum başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
} 