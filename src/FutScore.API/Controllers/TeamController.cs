using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Services.TeamService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeamController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TeamDto>>> GetAll()
        {
            var teams = await _teamService.GetAllAsync();
            return Ok(teams);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeamDto>> GetById(Guid id)
        {
            var team = await _teamService.GetByIdAsync(id);
            if (team == null)
                return NotFound();
            return Ok(team);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<TeamDetailDto>> GetDetail(Guid id)
        {
            var team = await _teamService.GetTeamDetailAsync(id);
            if (team == null)
                return NotFound();
            return Ok(team);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TeamDto>> Create([FromBody] TeamDto teamDto)
        {
            var result = await _teamService.AddAsync(teamDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = teamDto.Id }, teamDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<TeamDto>> Update(Guid id, [FromBody] TeamDto teamDto)
        {
            if (id != teamDto.Id)
                return BadRequest();

            var result = await _teamService.UpdateAsync(teamDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(teamDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _teamService.DeleteByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpGet("{id}/players")]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetTeamPlayers(Guid id)
        {
            var players = await _teamService.GetTeamPlayersAsync(id);
            return Ok(players);
        }

        [HttpGet("{id}/matches")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetTeamMatches(Guid id)
        {
            var matches = await _teamService.GetTeamMatchesAsync(id);
            return Ok(matches);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<TeamStatsDto>> GetTeamStats(Guid id)
        {
            var stats = await _teamService.GetTeamStatsAsync(id);
            if (stats == null)
                return NotFound();
            return Ok(stats);
        }

        [HttpPost("{id}/players/{playerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddPlayer(Guid id, Guid playerId)
        {
            var result = await _teamService.AddPlayerToTeamAsync(id, playerId);
            if (!result)
                return BadRequest(result);

            return NoContent();
        }

        [HttpDelete("{id}/players/{playerId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemovePlayer(Guid id, Guid playerId)
        {
            var result = await _teamService.RemovePlayerFromTeamAsync(id, playerId);
            if (!result)
                return BadRequest("Failed to remove player from team");

            return NoContent();
        }

        //[HttpPut("{id}/status")]
        //[Authorize(Roles = "Admin")]
        //public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        //{
        //    var result = await _teamService.UpdateTeamStatusAsync(id, status);
        //    if (!result)
        //        return BadRequest("Failed to update team status");

        //    return NoContent();
        //}
    }
} 