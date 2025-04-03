using AutoMapper;
using FutScore.Application.DTOs.Team;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using System;
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
            var teams = await _teamRepository.GetAllWithRelationsAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task<TeamDto> GetTeamByIdAsync(int id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDto>(team);
        }

        public async Task<ProcessResult> CreateTeamAsync(CreateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            return await _teamRepository.AddAsync(team);
        }

        public async Task<ProcessResult> UpdateTeamAsync(UpdateTeamDto teamDto)
        {
            var team = _mapper.Map<Team>(teamDto);
            return await _teamRepository.UpdateAsync(team);
        }

        public async Task<ProcessResult> DeleteTeamAsync(int teamId)
        {
            var team = await _teamRepository.GetByIdAsync(teamId);
            if (team == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Takım bulunamadı."
                };
            }

            return await _teamRepository.DeleteAsync(team);
        }
    }
}
