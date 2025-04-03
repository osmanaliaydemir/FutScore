using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Leagues
{
    public class EditModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public EditModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        [BindProperty]
        public UpdateLeagueDto League { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var leagues = await _leagueService.GetAllLeaguesAsync();
            var league = leagues.FirstOrDefault(l => l.Id == id);
            
            if (league == null)
            {
                return NotFound();
            }

            League = new UpdateLeagueDto
            {
                Id = league.Id,
                Name = league.Name,
                Country = league.Country,
                TeamCount = league.TeamCount,
                LogoUrl = league.LogoUrl,
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var result = await _leagueService.UpdateLeagueAsync(League);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Lig başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            return Page();
        }
    }
} 