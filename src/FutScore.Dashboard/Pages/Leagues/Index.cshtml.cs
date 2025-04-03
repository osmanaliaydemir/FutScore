using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Leagues
{
    public class IndexModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public IndexModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public IEnumerable<LeagueDto> Leagues { get; set; } = new List<LeagueDto>();

        public async Task OnGetAsync()
        {
            Leagues = await _leagueService.GetAllLeaguesAsync();
        }
    }
} 