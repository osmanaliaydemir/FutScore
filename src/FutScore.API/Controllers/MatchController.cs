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

        // GET: api/match
        [HttpGet]
        public async Task<IActionResult> GetAllMatches()
        {
            var matches = await _matchService.GetAllMatchesAsync();
            return Ok(matches);
        }

        // GET: api/match/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchById(int id)
        {
            var match = await _matchService.GetMatchByIdAsync(id);
            if (match == null)
            {
                return NotFound("Maç bulunamadý.");
            }
            return Ok(match);
        }

        // POST: api/match
        [HttpPost]
        public async Task<IActionResult> AddMatch([FromBody] MatchDto matchDto)
        {
            if (matchDto == null)
            {
                return BadRequest("Geçersiz maç verisi.");
            }

            var result = await _matchService.AddMatchAsync(matchDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetMatchById), new { id = result.EntityId }, matchDto);
            }

            return BadRequest(result.Message);
        }

        // PUT: api/match/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMatch(int id, [FromBody] MatchDto matchDto)
        {
            if (id != matchDto.Id)
            {
                return BadRequest("Maç ID'si eþleþmiyor.");
            }

            var result = await _matchService.UpdateMatchAsync(matchDto);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }

        // DELETE: api/match/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var result = await _matchService.DeleteMatchAsync(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }

            return BadRequest(result.Message);
        }
    }
} 