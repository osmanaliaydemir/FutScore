using FutScore.Application.DTOs.Match;
using FutScore.Application.DTOs.MatchEvent;
using FutScore.Application.Services.MatchService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;

        public MatchController(IMatchService matchService)
        {
            _matchService = matchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetAll()
        {
            var matches = await _matchService.GetAllAsync();
            return Ok(matches);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MatchDto>> GetById(Guid id)
        {
            var match = await _matchService.GetByIdAsync(id);
            if (match == null)
                return NotFound();
            return Ok(match);
        }

        [HttpGet("{id}/detail")]
        public async Task<ActionResult<MatchDetailDto>> GetDetail(Guid id)
        {
            var match = await _matchService.GetMatchDetailAsync(id);
            if (match == null)
                return NotFound();
            return Ok(match);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MatchDto>> Create([FromBody] MatchDto matchDto)
        {
            var result = await _matchService.AddAsync(matchDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return CreatedAtAction(nameof(GetById), new { id = matchDto.Id }, matchDto);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<MatchDto>> Update(Guid id, [FromBody] MatchDto matchDto)
        {
            if (id != matchDto.Id)
                return BadRequest();

            var result = await _matchService.UpdateAsync(matchDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(matchDto);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _matchService.DeleteByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return NoContent();
        }

        [HttpGet("{id}/events")]
        public async Task<ActionResult<IEnumerable<MatchEventDto>>> GetMatchEvents(Guid id)
        {
            var events = await _matchService.GetMatchEventsAsync(id);
            return Ok(events);
        }



        [HttpGet("upcoming")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetUpcomingMatches()
        {
            var matches = await _matchService.GetUpcomingMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("live")]
        public async Task<ActionResult<IEnumerable<MatchDto>>> GetLiveMatches()
        {
            var matches = await _matchService.GetLiveMatchesAsync();
            return Ok(matches);
        }

      

        [HttpPut("{id}/status")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] string status)
        {
            var result = await _matchService.UpdateMatchStatusAsync(id, status);
            if (!result)
                return BadRequest("Failed to update match status");

            return NoContent();
        }
    }
} 