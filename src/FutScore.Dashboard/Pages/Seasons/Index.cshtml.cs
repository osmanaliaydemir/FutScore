using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Season;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Antiforgery;

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
            try
            {
                var seasons = await _seasonService.GetAllSeasonsAsync();
                return new JsonResult(seasons);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnGetSeasonAsync(int id)
        {
            try
            {
                var season = await _seasonService.GetByIdAsync(id);
                if (season == null)
                    return new JsonResult(new { success = false, message = "Season not found" }) { StatusCode = 404 };

                return new JsonResult(season);
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnPostCreateSeasonAsync([FromBody] CreateSeasonDto seasonDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new JsonResult(new { success = false, message = "Invalid model state" }) { StatusCode = 400 };

                var result = await _seasonService.CreateSeasonAsync(seasonDto);
                return new JsonResult(new { success = true, data = result });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnPostUpdateSeasonAsync([FromBody] UpdateSeasonDto seasonDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return new JsonResult(new { success = false, message = "Invalid model state" }) { StatusCode = 400 };

                var result = await _seasonService.UpdateSeasonAsync(seasonDto);
                return new JsonResult(new { success = result.Success, data = result.Message });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message }) { StatusCode = 500 };
            }
        }

        public async Task<IActionResult> OnPostDeleteSeasonAsync(int id)
        {
            try
            {
                await _seasonService.DeleteSeasonAsync(id);
                return new JsonResult(new { success = true });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { success = false, message = ex.Message }) { StatusCode = 500 };
            }
        }
    }
} 