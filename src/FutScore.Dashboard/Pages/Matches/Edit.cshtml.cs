using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Season;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Dashboard.Pages.Matches
{
    public class EditModel : PageModel
    {
        private readonly IMatchService _matchService;
        private readonly ISeasonService _seasonService;
        private readonly ITeamService _teamService;

        public EditModel(IMatchService matchService, ISeasonService seasonService, ITeamService teamService)
        {
            _matchService = matchService;
            _seasonService = seasonService;
            _teamService = teamService;
        }

        [BindProperty]
        public UpdateMatchDto Match { get; set; }

        public SelectList Seasons { get; set; }
        public SelectList Teams { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            var seasons = await _seasonService.GetAllSeasonsAsync();
            var teams = await _teamService.GetAllTeamsAsync();

            Seasons = new SelectList(seasons, "Id", "Name");
            Teams = new SelectList(teams, "Id", "Name");

            Match = new UpdateMatchDto
            {
                Id = match.Id,
                SeasonId = match.SeasonId,
                HomeTeamId = match.HomeTeamId,
                AwayTeamId = match.AwayTeamId,
                MatchDate = match.MatchDate,
                Stadium = match.Stadium.Name,
                HomeTeamScore = match.HomeTeamScore,
                AwayTeamScore = match.AwayTeamScore,
                Status = match.Status
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await OnGetAsync(Match.Id);
                return Page();
            }

            var result = await _matchService.UpdateMatchAsync(Match);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Maç başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }

            ModelState.AddModelError("", result.Message);
            await OnGetAsync(Match.Id);
            return Page();
        }
    }
} 