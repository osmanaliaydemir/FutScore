using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FutScore.Dashboard.Pages.Matches
{
    public class DetailsModel : PageModel
    {
        private readonly IMatchService _matchService;

        public DetailsModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        public MatchDto Match { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Match = await _matchService.GetMatchByIdAsync(id);
            if (Match == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
} 