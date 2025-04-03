using Microsoft.AspNetCore.Mvc;
using FutScore.Application.Interfaces;
using FutScore.Application.DTOs.League;
using Microsoft.AspNetCore.Authorization;
using FutScore.Domain.Entities;
using AutoMapper;

namespace FutScore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class LeagueController : ControllerBase
    {
        private readonly ILeagueService _leagueService;
        private readonly IMapper _mapper;

        public LeagueController(ILeagueService leagueService, IMapper mapper)
        {
            _leagueService = leagueService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets all leagues
        /// </summary>
        /// <returns>List of leagues</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<LeagueDto>>> GetAllLeagues()
        {
            var leagues = await _leagueService.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<LeagueDto>>(leagues));
        }

        /// <summary>
        /// Gets a specific league by id
        /// </summary>
        /// <param name="id">League id</param>
        /// <returns>League details</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<LeagueDto>> GetLeagueById(int id)
        {
            var league = await _leagueService.GetByIdAsync(id);
            if (league == null)
                return NotFound();

            return Ok(_mapper.Map<LeagueDto>(league));
        }

        /// <summary>
        /// Creates a new league
        /// </summary>
        /// <param name="leagueDto">League data</param>
        /// <returns>Created league</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LeagueDto>> CreateLeague(LeagueDto leagueDto)
        {
            var league = _mapper.Map<League>(leagueDto);
            var createdLeague = await _leagueService.AddAsync(league);
            return CreatedAtAction(nameof(GetLeagueById), new { id = createdLeague.Id }, _mapper.Map<LeagueDto>(createdLeague));
        }

        /// <summary>
        /// Updates an existing league
        /// </summary>
        /// <param name="id">League id</param>
        /// <param name="leagueDto">Updated league data</param>
        /// <returns>No content</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateLeague(int id, LeagueDto leagueDto)
        {
            if (id != leagueDto.Id)
                return BadRequest();

            var league = await _leagueService.GetByIdAsync(id);
            if (league == null)
                return NotFound();

            _mapper.Map(leagueDto, league);
            await _leagueService.UpdateAsync(league);

            return NoContent();
        }

        /// <summary>
        /// Deletes a league
        /// </summary>
        /// <param name="id">League id</param>
        /// <returns>No content</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteLeague(int id)
        {
            var league = await _leagueService.GetByIdAsync(id);
            if (league == null)
                return NotFound();

            await _leagueService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("withSeasons/{id}")]
        public async Task<ActionResult<League>> GetLeagueWithSeasons(int id)
        {
            var league = await _leagueService.GetLeagueWithSeasonsAsync(id);
            if (league == null)
                return NotFound();

            return Ok(league);
        }

        [HttpGet("byCountry/{country}")]
        public async Task<ActionResult<IEnumerable<League>>> GetLeaguesByCountry(string country)
        {
            var leagues = await _leagueService.GetLeaguesByCountryAsync(country);
            return Ok(leagues);
        }

        [HttpGet("checkName")]
        public async Task<ActionResult<bool>> IsLeagueNameUnique(string name, int? excludeId = null)
        {
            var isUnique = await _leagueService.IsLeagueNameUniqueAsync(name, excludeId);
            return Ok(isUnique);
        }
    }
} 