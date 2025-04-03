using FutScore.Application.DTOs.Player;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        // GET: api/player
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerDto>>> GetPlayers()
        {
            var players = await _playerService.GetAllPlayersAsync();
            if (players == null)
            {
                return NotFound("Oyuncular bulunamadı.");
            }
            return Ok(players);
        }

        // GET: api/player/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerDto>> GetPlayerById(int id)
        {
            var player = await _playerService.GetPlayerByIdAsync(id);
            if (player == null)
            {
                return NotFound("Oyuncu bulunamadı.");
            }
            return Ok(player);
        }

        // POST: api/player
        [HttpPost]
        public async Task<ActionResult<ProcessResult>> AddPlayer([FromBody] PlayerDto playerDto)
        {
            var result = await _playerService.AddPlayerAsync(playerDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetPlayerById), new { id = playerDto.Id }, result);
            }
            return BadRequest(result.Message);
        }

        // PUT: api/player/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProcessResult>> UpdatePlayer(int id, [FromBody] PlayerDto playerDto)
        {
            if (id != playerDto.Id)
            {
                return BadRequest("ID uyuşmazlığı.");
            }

            var result = await _playerService.UpdatePlayerAsync(playerDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // DELETE: api/player/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult>> DeletePlayer(int id)
        {
            var result = await _playerService.DeletePlayerAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
