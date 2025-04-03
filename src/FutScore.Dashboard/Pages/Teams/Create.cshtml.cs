using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutScore.Dashboard.Pages.Teams
{
    public class CreateModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ILeagueService _leagueService;
        private readonly IStadiumService _stadiumService;

        public CreateModel(ITeamService teamService, ILeagueService leagueService, IStadiumService stadiumService)
        {
            _teamService = teamService;
            _leagueService = leagueService;
            _stadiumService = stadiumService;
        }

        [BindProperty]
        public CreateTeamDto Team { get; set; }

        public SelectList Leagues { get; set; }
        public SelectList Stadiums { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var leagues = await _leagueService.GetAllLeaguesAsync();
            var stadiums = await _stadiumService.GetAllStadiumsAsync();

            Leagues = new SelectList(leagues, "Id", "Name");
            Stadiums = new SelectList(stadiums, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return await OnGetAsync();
            }

            try
            {
                var result = await _teamService.CreateTeamAsync(Team);
                if (result.Success)
                {
                    TempData["SuccessMessage"] = "Takım başarıyla eklendi.";
                }
                else
                {
                    TempData["SuccessMessage"] = result.Message;
                }
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Takım eklenirken bir hata oluştu: {ex.Message}");
                return await OnGetAsync();
            }
        }
    }
}