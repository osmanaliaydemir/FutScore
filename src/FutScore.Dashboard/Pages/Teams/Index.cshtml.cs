using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Teams
{
    public class IndexModel : PageModel
    {
        private readonly ITeamService _teamService;

        public IndexModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        public IEnumerable<TeamDto> Teams { get; set; } = new List<TeamDto>();

        public async Task OnGetAsync()
        {
            Teams = await _teamService.GetAllTeamsAsync();
        }
    }
} 