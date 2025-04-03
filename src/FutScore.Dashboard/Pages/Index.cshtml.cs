using Microsoft.AspNetCore.Mvc.RazorPages;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Season;

namespace FutScore.Dashboard.Pages;

public class IndexModel : PageModel
{
    private readonly ILeagueService _leagueService;
    private readonly ITeamService _teamService;
    private readonly IMatchService _matchService;
    private readonly ISeasonService _seasonService;

    public int LeaguesCount { get; set; }
    public int TeamsCount { get; set; }
    public int MatchesCount { get; set; }
    public int ActiveSeasonsCount { get; set; }
    public List<MatchDto> RecentMatches { get; set; } = new();
    public List<SeasonDto> ActiveSeasons { get; set; } = new();

    public IndexModel(
        ILeagueService leagueService,
        ITeamService teamService,
        IMatchService matchService,
        ISeasonService seasonService)
    {
        _leagueService = leagueService;
        _teamService = teamService;
        _matchService = matchService;
        _seasonService = seasonService;
    }

    public async Task OnGetAsync()
    {
        LeaguesCount = (await _leagueService.GetAllLeaguesAsync()).Count();
        TeamsCount = (await _teamService.GetAllTeamsAsync()).Count();
        MatchesCount = (await _matchService.GetAllMatchesAsync()).Count();
        
        var allSeasons = await _seasonService.GetAllSeasonsAsync();
        ActiveSeasonsCount = allSeasons.Count();
        
        RecentMatches = (await _matchService.GetAllMatchesAsync())
            .OrderByDescending(m => m.MatchDate)
            .Take(10)
            .ToList();
            
        ActiveSeasons = allSeasons
            .OrderBy(s => s.StartDate)
            .Take(5)
            .ToList();
    }
}
