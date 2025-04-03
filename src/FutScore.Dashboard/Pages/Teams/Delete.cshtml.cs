using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FutScore.Dashboard.Pages.Teams
{
    public class DeleteModel : PageModel
    {
        private readonly ITeamService _teamService;

        public DeleteModel(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            try
            {
                await _teamService.DeleteTeamAsync(id);
                TempData["SuccessMessage"] = "Takım başarıyla silindi.";
                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Takım silinirken bir hata oluştu: {ex.Message}";
                return RedirectToPage("./Index");
            }
        }
    }
} 