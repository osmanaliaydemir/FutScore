using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FutScore.Dashboard.Pages.Teams
{
    public class EditModel : PageModel
    {
        private readonly ITeamService _teamService;
        private readonly ILeagueService _leagueService;
        private readonly IStadiumService _stadiumService;

        public EditModel(ITeamService teamService, ILeagueService leagueService, IStadiumService stadiumService)
        {
            _teamService = teamService;
            _leagueService = leagueService;
            _stadiumService = stadiumService;
        }

        [BindProperty]
        public UpdateTeamDto Team { get; set; }

        public SelectList Leagues { get; set; }
        public SelectList Stadiums { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
            {
                return NotFound();
            }

            var leagues = await _leagueService.GetAllLeaguesAsync();
            var stadiums = await _stadiumService.GetAllStadiumsAsync();

            Leagues = new SelectList(leagues, "Id", "Name");
            Stadiums = new SelectList(stadiums, "Id", "Name");

            Team = new UpdateTeamDto
            {
                Id = team.Id,
                Name = team.Name,
                LeagueId = team.LeagueId,
                StadiumId = team.StadiumId,
                City = team.City,
                LogoUrl = team.LogoUrl
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                var leagues = await _leagueService.GetAllLeaguesAsync();
                var stadiums = await _stadiumService.GetAllStadiumsAsync();

                Leagues = new SelectList(leagues, "Id", "Name");
                Stadiums = new SelectList(stadiums, "Id", "Name");

                return Page();
            }

            try
            {
                await _teamService.UpdateTeamAsync(Team);
                TempData["SuccessMessage"] = "Takım başarıyla güncellendi.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Takım güncellenirken bir hata oluştu: {ex.Message}");
                return await OnGetAsync(Team.Id);
            }
        }
    }
} 