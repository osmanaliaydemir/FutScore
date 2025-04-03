using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Leagues
{
    public class CreateModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public CreateModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [BindProperty]
        public CreateLeagueDto League { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _leagueService.CreateLeagueAsync(League);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Lig başarıyla oluşturuldu.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
} 