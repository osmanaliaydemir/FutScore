using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace FutScore.Dashboard.Pages.Matches
{
    public class DeleteModel : PageModel
    {
        private readonly IMatchService _matchService;

        public DeleteModel(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            var result = await _matchService.DeleteMatchAsync(id);
            if (result.Success)
            {
                TempData["SuccessMessage"] = "Maç başarıyla silindi.";
                return RedirectToPage("./Index");
            }

            TempData["ErrorMessage"] = result.Message;
            return RedirectToPage("./Index");
        }
    }
} 