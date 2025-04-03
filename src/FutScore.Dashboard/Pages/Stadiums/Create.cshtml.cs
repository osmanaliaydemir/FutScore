using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Stadiums
{
    public class CreateModel : PageModel
    {
        private readonly IStadiumService _stadiumService;

        public CreateModel(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        [BindProperty]
        public CreateStadiumDto Stadium { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _stadiumService.AddStadiumAsync(Stadium);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Stadyum başarıyla oluşturuldu.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
} 