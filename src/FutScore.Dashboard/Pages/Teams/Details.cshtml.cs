using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Teams
{
    public class DetailsModel : PageModel
    {
        private readonly ITeamService _teamService;

        public DetailsModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public TeamDto Team { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Team = await _teamService.GetTeamByIdAsync(id);

            if (Team == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
} 