using AutoMapper;
using FutScore.Application.DTOs.League;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Application.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;

        public LeagueService(ILeagueRepository leagueRepository, IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<LeagueDto>> GetAllLeaguesAsync()
        {
            var leagues = await _leagueRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LeagueDto>>(leagues);
        }

        public async Task<ProcessResult> CreateLeagueAsync(LeagueDto leagueDto)
        {
            var league = _mapper.Map<League>(leagueDto);
            return await _leagueRepository.AddAsync(league);
        }

        public async Task<ProcessResult> UpdateLeagueAsync(LeagueDto leagueDto)
        {
            var existingLeague = await _leagueRepository.GetByIdAsync(leagueDto.Id);
            if (existingLeague == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Lig bulunamadý."
                };
            }

            _mapper.Map(leagueDto, existingLeague);
            return await _leagueRepository.UpdateAsync(existingLeague);
        }

        public async Task<ProcessResult> DeleteLeagueAsync(int leagueId)
        {
            var league = await _leagueRepository.GetByIdAsync(leagueId);
            if (league == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Lig bulunamadý."
                };
            }

            return await _leagueRepository.DeleteAsync(league);
        }
    }
}
