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

        [HttpGet]
        public async Task<IActionResult> GetTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            return Ok(teams);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTeam([FromBody] CreateTeamDto teamDto)
        {
            var result = await _teamService.CreateTeamAsync(teamDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateTeam([FromBody] UpdateTeamDto teamDto)
        {
            var result = await _teamService.UpdateTeamAsync(teamDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            var result = await _teamService.DeleteTeamAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
} 