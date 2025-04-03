using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumController : ControllerBase
    {
        private readonly IStadiumService _stadiumService;

        public StadiumController(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }

        // GET: api/stadium
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StadiumDto>>> GetStadiums()
        {
            var stadiums = await _stadiumService.GetAllStadiumsAsync();
            if (stadiums == null)
            {
                return NotFound("Stadyumlar bulunamadı.");
            }
            return Ok(stadiums);
        }

        // GET: api/stadium/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<StadiumDto>> GetStadiumById(int id)
        {
            var stadium = await _stadiumService.GetStadiumByIdAsync(id);
            if (stadium == null)
            {
                return NotFound("Stadyum bulunamadı.");
            }
            return Ok(stadium);
        }

        // POST: api/stadium
        [HttpPost]
        public async Task<ActionResult<ProcessResult>> AddStadium([FromBody] CreateStadiumDto stadiumDto)
        {
            var result = await _stadiumService.AddStadiumAsync(stadiumDto);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetStadiumById), new { id = result.EntityId }, result);
            }
            return BadRequest(result.Message);
        }

        // PUT: api/stadium/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<ProcessResult>> UpdateStadium(int id, [FromBody] UpdateStadiumDto stadiumDto)
        {
            if (id != stadiumDto.Id)
            {
                return BadRequest("ID uyuşmazlığı.");
            }

            var result = await _stadiumService.UpdateStadiumAsync(stadiumDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }

        // DELETE: api/stadium/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessResult>> DeleteStadium(int id)
        {
            var result = await _stadiumService.DeleteStadiumAsync(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result.Message);
        }
    }
}
