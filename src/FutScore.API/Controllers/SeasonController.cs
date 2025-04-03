using Microsoft.AspNetCore.Mvc;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Season;
using Microsoft.AspNetCore.Authorization;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class SeasonController : ControllerBase
    {
        private readonly ISeasonService _seasonService;

        public SeasonController(ISeasonService seasonService)
        {
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetSeasons()
        {
            var seasons = await _seasonService.GetAllSeasonsAsync();
            return Ok(seasons);
        }

        [HttpPost]
        public async Task<IActionResult> CreateSeason([FromBody] CreateSeasonDto seasonDto)
        {
            var result = await _seasonService.CreateSeasonAsync(seasonDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSeason([FromBody] UpdateSeasonDto seasonDto)
        {
            var result = await _seasonService.UpdateSeasonAsync(seasonDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            var result = await _seasonService.DeleteSeasonAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

    }
} 