using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutScore.Dashboard.Pages.Matches
{
    public class CreateModel : PageModel
    {
        private readonly IMatchService _matchService;
        private readonly ISeasonService _seasonService;
        private readonly ITeamService _teamService;
        private readonly IStadiumService _stadiumService;

        public CreateModel(IMatchService matchService, ISeasonService seasonService, ITeamService teamService, IStadiumService stadiumService)
        {
            _matchService = matchService;
            _seasonService = seasonService;
            _teamService = teamService;
            _stadiumService = stadiumService;
        }

        [BindProperty]
        public CreateMatchDto Match { get; set; }

        public SelectList Seasons { get; set; }
        public SelectList Teams { get; set; }
        public SelectList Stadium { get; set; }

        public async Task OnGetAsync()
        {
            var seasons = await _seasonService.GetAllSeasonsAsync();
            var teams = await _teamService.GetAllTeamsAsync();
            var stadium = await _stadiumService.GetAllStadiumsAsync();

            Seasons = new SelectList(seasons, "Id", "SeasonName");
            Teams = new SelectList(teams, "Id", "Name");
            Stadium = new SelectList(stadium, "Id", "Name");
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync();
                return Page();
            }

            var result = await _matchService.AddMatchAsync(Match);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Maç başarıyla eklendi.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            await OnGetAsync();
            return Page();
        }
    }
}