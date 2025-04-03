using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Stadiums
{
    public class DeleteModel : PageModel
    {
        private readonly IStadiumService _stadiumService;

        public DeleteModel(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _stadiumService.DeleteStadiumAsync(id);
                TempData["SuccessMessage"] = "Stadyum başarıyla silindi.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Stadyum silinirken bir hata oluştu: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }
    }
} 