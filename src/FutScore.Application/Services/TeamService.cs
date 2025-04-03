using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;

namespace FutScore.Application.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDto>> GetAllTeamsAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<TeamDto> CreateTeamAsync(CreateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            await _teamRepository.AddAsync(team);
            await _teamRepository.SaveChangesAsync();
            return _mapper.Map<TeamDto>(team);
        }

        public async Task UpdateTeamAsync(UpdateTeamDto teamDto)
        {
            var team = await _teamRepository.GetByIdAsync(teamDto.Id);
            if (team == null)
                throw new KeyNotFoundException($"Team with ID {teamDto.Id} not found.");

            _mapper.Map(teamDto, team);
            _teamRepository.Update(team);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task DeleteTeamAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            if (team == null)
                throw new KeyNotFoundException($"Team with ID {id} not found.");

            _teamRepository.Delete(team);
            await _teamRepository.SaveChangesAsync();
        }

        public async Task<Team> GetTeamWithPlayersAsync(int teamId)
        {
            return await _teamRepository.GetTeamWithPlayersAsync(teamId);
        }

        public async Task<Team> GetTeamWithMatchesAsync(int teamId)
        {
            return await _teamRepository.GetTeamWithMatchesAsync(teamId);
        }

        public async Task<Team> GetTeamWithStandingsAsync(int teamId)
        {
            return await _teamRepository.GetTeamWithStandingsAsync(teamId);
        }

        public async Task<IEnumerable<Team>> GetTeamsBySeasonAsync(int seasonId)
        {
            return await _teamRepository.GetTeamsBySeasonAsync(seasonId);
        }

        public async Task<bool> IsTeamInSeasonAsync(int teamId, int seasonId)
        {
            return await _teamRepository.IsTeamInSeasonAsync(teamId, seasonId);
        }

        public async Task AddTeamToSeasonAsync(int teamId, int seasonId)
        {
            await _teamRepository.AddTeamToSeasonAsync(teamId, seasonId);
        }

        public async Task RemoveTeamFromSeasonAsync(int teamId, int seasonId)
        {
            await _teamRepository.RemoveTeamFromSeasonAsync(teamId, seasonId);
        }
    }
} 