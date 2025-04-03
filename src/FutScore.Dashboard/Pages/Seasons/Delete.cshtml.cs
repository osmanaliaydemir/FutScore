using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Seasons
{
    public class DeleteModel : PageModel
    {
        private readonly ISeasonService _seasonService;

        public DeleteModel(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _seasonService.DeleteSeasonAsync(id);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Sezon başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToPage("./Index");
        }
    }
} 