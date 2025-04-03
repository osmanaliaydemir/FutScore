using FutScore.Application.DTOs.Season;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Seasons
{
    public class DetailsModel : PageModel
    {
        private readonly ISeasonService _seasonService;
        private readonly ILeagueService _leagueService;

        public DetailsModel(ISeasonService seasonService, ILeagueService leagueService)
        {
            _seasonService = seasonService;
            _leagueService = leagueService;
        }

        public SeasonDto Season { get; set; }
        public string LeagueName { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Season = await _seasonService.GetByIdAsync(id);
            if (Season == null)
            {
                return NotFound();
            }

            var leagues = await _leagueService.GetAllLeaguesAsync();
            var league = leagues.FirstOrDefault(l => l.Id == Season.LeagueId);
            LeagueName = league?.Name ?? "Bilinmiyor";

            return Page();
        }
    }
} 