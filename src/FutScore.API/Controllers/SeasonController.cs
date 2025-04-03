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

        /// <summary>
        /// Gets all seasons
        /// </summary>
        /// <returns>List of seasons</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetAllSeasons()
        {
            var seasons = await _seasonService.GetAllSeasonsAsync();
            return Ok(seasons);
        }

        /// <summary>
        /// Gets seasons by league id
        /// </summary>
        /// <param name="leagueId">League id</param>
        /// <returns>List of seasons for the league</returns>
        [HttpGet("league/{leagueId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SeasonDto>>> GetSeasonsByLeague(int leagueId)
        {
            var seasons = await _seasonService.GetSeasonsByLeagueAsync(leagueId);
            return Ok(seasons);
        }

        /// <summary>
        /// Gets a specific season by id
        /// </summary>
        /// <param name="id">Season id</param>
        /// <returns>Season details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SeasonDto>> GetSeasonById(int id)
        {
            var season = await _seasonService.GetSeasonByIdAsync(id);
            if (season == null)
                return NotFound();

            return Ok(season);
        }

        /// <summary>
        /// Creates a new season
        /// </summary>
        /// <param name="seasonDto">Season data</param>
        /// <returns>Created season</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SeasonDto>> CreateSeason([FromBody] CreateSeasonDto seasonDto)
        {
            var createdSeason = await _seasonService.CreateSeasonAsync(seasonDto);
            return CreatedAtAction(nameof(GetSeasonById), new { id = createdSeason.Id }, createdSeason);
        }

        /// <summary>
        /// Updates an existing season
        /// </summary>
        /// <param name="id">Season id</param>
        /// <param name="seasonDto">Updated season data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSeason(int id, [FromBody] UpdateSeasonDto seasonDto)
        {
            if (id != seasonDto.Id)
                return BadRequest();

            await _seasonService.UpdateSeasonAsync(seasonDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a season
        /// </summary>
        /// <param name="id">Season id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSeason(int id)
        {
            await _seasonService.DeleteSeasonAsync(id);
            return NoContent();
        }
    }
} 