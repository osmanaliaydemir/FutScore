using FutScore.Application.DTOs.League;
using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.Player;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Services.LeagueService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;

        public LeagueController(ILeagueService leagueService)
        {
            _leagueService = leagueService;
        }

        // CRUD Operations
        [HttpPost]
        public async Task<ActionResult<LeagueDto>> CreateLeague([FromBody] LeagueCreateDto createDto)
        {
            var league = await _leagueService.CreateLeagueAsync(createDto);
            return CreatedAtAction(nameof(GetById), new { id = league.Id }, league);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LeagueDto>> UpdateLeague(Guid id, [FromBody] LeagueUpdateDto updateDto)
        {
            var league = await _leagueService.UpdateLeagueAsync(id, updateDto);
            if (league == null)
                return NotFound();
            return Ok(league);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLeague(Guid id)
        {
            var result = await _leagueService.DeleteLeagueAsync(id);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetAll()
        {
            var leagues = await _leagueService.GetAllLeaguesAsync();
            return Ok(leagues);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LeagueDto>> GetById(Guid id)
        {
            var league = await _leagueService.GetLeagueByIdAsync(id);
            if (league == null)
                return NotFound();
            return Ok(league);
        }

        // Additional Operations

        [HttpGet("country/{country}")]
        public async Task<ActionResult<List<LeagueDto>>> GetLeaguesByCountry(string country)
        {
            var leagues = await _leagueService.GetLeaguesByCountryAsync(country);
            return Ok(leagues);
        }

        [HttpPut("{id}/status")]
        public async Task<ActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            var result = await _leagueService.UpdateLeagueStatusAsync(id, status);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpPost("{leagueId}/teams/{teamId}")]
        public async Task<ActionResult> AddTeam(Guid leagueId, Guid teamId)
        {
            var result = await _leagueService.AddTeamToLeagueAsync(leagueId, teamId);
            if (!result.Success)
                return BadRequest(result.Message);
            return NoContent();
        }

        [HttpDelete("{leagueId}/teams/{teamId}")]
        public async Task<ActionResult> RemoveTeam(Guid leagueId, Guid teamId)
        {
            var result = await _leagueService.RemoveTeamFromLeagueAsync(leagueId, teamId);
            if (!result)
                return NotFound();
            return NoContent();
        }

        [HttpGet("{id}/teams")]
        public async Task<ActionResult<List<TeamDto>>> GetTeams(Guid id)
        {
            var teams = await _leagueService.GetLeagueTeamsAsync(id);
            return Ok(teams);
        }

        [HttpGet("{id}/matches")]
        public async Task<ActionResult<List<MatchDto>>> GetMatches(Guid id)
        {
            var matches = await _leagueService.GetLeagueMatchesAsync(id);
            return Ok(matches);
        }

        [HttpGet("{id}/players")]
        public async Task<ActionResult<List<PlayerDto>>> GetPlayers(Guid id)
        {
            var players = await _leagueService.GetLeaguePlayersAsync(id);
            return Ok(players);
        }

        [HttpGet("{id}/stats")]
        public async Task<ActionResult<LeagueStatsDto>> GetStats(Guid id)
        {
            var stats = await _leagueService.GetLeagueStatsAsync(id);
            if (stats == null)
                return NotFound();
            return Ok(stats);
        }
    }
} 