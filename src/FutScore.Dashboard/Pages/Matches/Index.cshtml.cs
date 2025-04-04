using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Season;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Dashboard.Pages.Matches
{
    public class IndexModel : PageModel
    {
        private readonly IMatchService _matchService;
        private readonly ISeasonService _seasonService;
        private readonly ITeamService _teamService;

        public IndexModel(IMatchService matchService, ISeasonService seasonService, ITeamService teamService)
        {
            _matchService = matchService;
            _seasonService = seasonService;
            _teamService = teamService;
        }

        public IEnumerable<MatchDto> Matches { get; set; }
        public IEnumerable<SeasonDto> Seasons { get; set; }
        public IEnumerable<TeamDto> Teams { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? SeasonId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? TeamId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Status { get; set; }

        public async Task OnGetAsync()
        {
            Seasons = await _seasonService.GetAllSeasonsAsync();
            Teams = await _teamService.GetAllTeamsAsync();
            Matches = await _matchService.GetAllMatchesAsync();

            if (SeasonId.HasValue)
            {
                Matches = Matches.Where(m => m.SeasonId == SeasonId.Value);
            }

            if (TeamId.HasValue)
            {
                Matches = Matches.Where(m => m.HomeTeamId == TeamId.Value || m.AwayTeamId == TeamId.Value);
            }

            if (!string.IsNullOrEmpty(Status))
            {
                Matches = Matches.Where(m => m.Status == Status);
            }
        }
    }
} 