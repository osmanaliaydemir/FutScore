using Microsoft.AspNetCore.Mvc;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.Team;
using Microsoft.AspNetCore.Authorization;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        /// <summary>
        /// Gets all teams
        /// </summary>
        /// <returns>List of teams</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        /// <summary>
        /// Gets teams by season id
        /// </summary>
        /// <param name="seasonId">Season id</param>
        /// <returns>List of teams in the season</returns>
        [HttpGet("season/{seasonId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetTeamsBySeason(int seasonId)
        {
            var teams = await _teamService.GetTeamsBySeasonAsync(seasonId);
            return Ok(teams);
        }

        /// <summary>
        /// Gets a specific team by id
        /// </summary>
        /// <param name="id">Team id</param>
        /// <returns>Team details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<TeamDto>> GetTeamById(int id)
        {
            var team = await _teamService.GetTeamByIdAsync(id);
            if (team == null)
                return NotFound();

            return Ok(team);
        }

        /// <summary>
        /// Creates a new team
        /// </summary>
        /// <param name="teamDto">Team data</param>
        /// <returns>Created team</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<TeamDto>> CreateTeam([FromBody] CreateTeamDto teamDto)
        {
            var createdTeam = await _teamService.CreateTeamAsync(teamDto);
            return CreatedAtAction(nameof(GetTeamById), new { id = createdTeam.Id }, createdTeam);
        }

        /// <summary>
        /// Updates an existing team
        /// </summary>
        /// <param name="id">Team id</param>
        /// <param name="teamDto">Updated team data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTeam(int id, [FromBody] UpdateTeamDto teamDto)
        {
            if (id != teamDto.Id)
                return BadRequest();

            await _teamService.UpdateTeamAsync(teamDto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a team
        /// </summary>
        /// <param name="id">Team id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            await _teamService.DeleteTeamAsync(id);
            return NoContent();
        }
    }
} 