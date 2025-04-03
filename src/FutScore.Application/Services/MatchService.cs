using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FutScore.Domain;

namespace FutScore.Application.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult> AddMatchAsync(MatchDto matchDto)
        {
            var match = _mapper.Map<Match>(matchDto);
            var result = await _matchRepository.AddAsync(match);
            return result;
        }

        public async Task<ProcessResult> UpdateMatchAsync(MatchDto matchDto)
        {
            var existingMatch = await _matchRepository.GetByIdAsync(matchDto.Id);
            if (existingMatch == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Maç bulunamadý."
                };
            }

            _mapper.Map(matchDto, existingMatch);
            return await _matchRepository.UpdateAsync(existingMatch);
        }

        public async Task<ProcessResult> DeleteMatchAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Maç bulunamadý."
                };
            }

            return await _matchRepository.DeleteAsync(match);
        }

        public async Task<IEnumerable<MatchDto>> GetAllMatchesAsync()
        {
            var matches = await _matchRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }

        public async Task<MatchDto> GetMatchByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            return _mapper.Map<MatchDto>(match);
        }
    }
}
