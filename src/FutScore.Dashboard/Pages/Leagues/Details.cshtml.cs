using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Leagues
{
    public class DetailsModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public DetailsModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public LeagueDto League { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var leagues = await _leagueService.GetAllLeaguesAsync();
            League = leagues.FirstOrDefault(l => l.Id == id);

            if (League == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
} 