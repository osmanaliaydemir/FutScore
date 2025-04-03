using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Season;

namespace FutScore.Dashboard.Pages.Seasons
{
    public class IndexModel : PageModel
    {
        private readonly ISeasonService _seasonService;

        public IndexModel(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnGetSeasonsAsync()
        {
            var seasons = await _seasonService.GetAllSeasonsAsync();
            return new JsonResult(seasons);
        }

        public async Task<IActionResult> OnGetSeasonAsync(int id)
        {
            var season = await _seasonService.GetSeasonByIdAsync(id);
            return new JsonResult(season);
        }

        public async Task<IActionResult> OnPostCreateSeasonAsync([FromBody] CreateSeasonDto seasonDto)
        {
            var season = await _seasonService.CreateSeasonAsync(seasonDto);
            return new JsonResult(season);
        }

        public async Task<IActionResult> OnPutUpdateSeasonAsync(int id, [FromBody] UpdateSeasonDto seasonDto)
        {
            await _seasonService.UpdateSeasonAsync(seasonDto);
            return new OkResult();
        }

        public async Task<IActionResult> OnDeleteSeasonAsync(int id)
        {
            await _seasonService.DeleteSeasonAsync(id);
            return new OkResult();
        }
    }
} 