using FutScore.Application.DTOs.Match;
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

        public IndexModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public IEnumerable<MatchDto> Matches { get; set; }

        public async Task OnGetAsync()
        {
            Matches = await _matchService.GetAllMatchesAsync();
        }
    }
} 