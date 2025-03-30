using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;
using FutScore.Application.DTOs.League;
using FutScore.Application.Services.LeagueService;

namespace FutScore.Dashboard.Pages.Leagues
{
    //[Authorize(Roles = "Admin")]
    public class IndexModel : PageModel
    {
        private readonly ILeagueService _leagueService;

        public IndexModel(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        public IEnumerable<LeagueDto> Leagues { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Leagues = await _leagueService.GetAllLeaguesAsync();
            return Page();
        }

        public async Task<IActionResult> OnPostCreateAsync([FromForm] LeagueCreateDto league)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _leagueService.CreateLeagueAsync(league);
            return new JsonResult(new { success = true, message = "Lig başarıyla oluşturuldu." });
        }

        public async Task<IActionResult> OnPostEditAsync(Guid id,[FromForm] LeagueUpdateDto league)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _leagueService.UpdateLeagueAsync(id,league);
            return new JsonResult(new { success = true, message = "Lig başarıyla güncellendi." });
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            await _leagueService.DeleteLeagueAsync(id);
            return new JsonResult(new { success = true, message = "Lig başarıyla silindi." });
        }
    }
} 