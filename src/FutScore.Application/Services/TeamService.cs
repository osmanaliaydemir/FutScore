using AutoMapper;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task<ProcessResult> CreateTeamAsync(CreateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            return await _teamRepository.AddAsync(team);
        }

        public async Task<ProcessResult> UpdateTeamAsync(UpdateTeamDto teamDto)
        {
            var existingTeam = await _teamRepository.GetByIdAsync(teamDto.Id);
            if (existingTeam == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Takým bulunamadý."
                };
            }

            _mapper.Map(teamDto, existingTeam);
            return await _teamRepository.UpdateAsync(existingTeam);
        }

        public async Task<ProcessResult> DeleteTeamAsync(int teamId)
        {
            var team = await _teamRepository.GetByIdAsync(teamId);
            if (team == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Takým bulunamadý."
                };
            }

            return await _teamRepository.DeleteAsync(team);
        }
    }
}
