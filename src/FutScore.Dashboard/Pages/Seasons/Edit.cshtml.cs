using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Season;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutScore.Dashboard.Pages.Seasons
{
    public class EditModel : PageModel
    {
        private readonly ISeasonService _seasonService;
        private readonly ILeagueService _leagueService;

        public EditModel(ISeasonService seasonService, ILeagueService leagueService)
        {
            _seasonService = seasonService;
            _leagueService = leagueService;
        }

        [BindProperty]
        public UpdateSeasonDto Season { get; set; } = new();

        public SelectList Leagues { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var season = await _seasonService.GetByIdAsync(id);
            if (season == null)
            {
                return NotFound();
            }

            Season = new UpdateSeasonDto
            {
                Id = season.Id,
                LeagueId = season.LeagueId,
                SeasonName = season.SeasonName,
                StartDate = season.StartDate,
                EndDate = season.EndDate
            };

            var leagues = await _leagueService.GetAllLeaguesAsync();
            Leagues = new SelectList(leagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var getLeagues = await _leagueService.GetAllLeaguesAsync();
                Leagues = new SelectList(getLeagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));
                return Page();
            }

            var result = await _seasonService.UpdateSeasonAsync(Season);

            if (result.Success)
            {
                TempData["SuccessMessage"] = "Sezon başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            var leagues = await _leagueService.GetAllLeaguesAsync();
            Leagues = new SelectList(leagues, nameof(LeagueDto.Id), nameof(LeagueDto.Name));
            return Page();
        }
    }
} 