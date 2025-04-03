using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Leagues
{
    public class DeleteModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public DeleteModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var result = await _leagueService.DeleteLeagueAsync(id);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Lig başarıyla silindi.";
            }
            else
            {
                TempData["ErrorMessage"] = result.Message;
            }

            return RedirectToPage("./Index");
        }
    }
} 