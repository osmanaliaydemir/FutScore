using Microsoft.AspNetCore.Mvc;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Match;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        /// <summary>
        /// Gets all matches
        /// </summary>
        /// <returns>List of matches</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAllMatches()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        /// <summary>
        /// Gets matches by season id
        /// </summary>
        /// <param name="seasonId">Season id</param>
        /// <returns>List of matches in the season</returns>
        [HttpGet("season/{seasonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesBySeason(int seasonId)
        {
            var matches = await _matchService.GetMatchesBySeasonAsync(seasonId);
            return Ok(matches);
        }

        /// <summary>
        /// Gets matches by team id
        /// </summary>
        /// <param name="teamId">Team id</param>
        /// <returns>List of matches for the team</returns>
        [HttpGet("team/{teamId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetMatchesByTeam(int teamId)
        {
            var matches = await _matchService.GetMatchesByTeamAsync(teamId);
            return Ok(matches);
        }

        /// <summary>
        /// Gets a specific match by id
        /// </summary>
        /// <param name="id">Match id</param>
        /// <returns>Match details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<MatchDto>> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
                return NotFound();

            return Ok(match);
        }

        /// <summary>
        /// Creates a new match
        /// </summary>
        /// <param name="matchDto">Match data</param>
        /// <returns>Created match</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<MatchDto>> CreateMatch([FromBody] CreateMatchDto matchDto)
        {
            var createdMatch = await _matchService.CreateMatchAsync(matchDto);
            return CreatedAtAction(nameof(GetMatchById), new { id = createdMatch.Id }, createdMatch);
        }

        /// <summary>
        /// Updates an existing match
        /// </summary>
        /// <param name="id">Match id</param>
        /// <param name="matchDto">Updated match data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateMatch(int id, [FromBody] UpdateMatchDto matchDto)
        {
            if (id != matchDto.Id)
                return BadRequest();

            await _matchService.UpdateMatchAsync(matchDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a match
        /// </summary>
        /// <param name="id">Match id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            await _matchService.DeleteMatchAsync(id);
            return NoContent();
        }
    }
} 