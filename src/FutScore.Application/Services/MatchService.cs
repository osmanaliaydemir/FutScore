using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FutScore.Domain;
using System;

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

        public async Task<ProcessResult> AddMatchAsync(CreateMatchDto matchDto)
        {
            try
            {
                if (matchDto.HomeTeamId == matchDto.AwayTeamId)
                {
                    return new ProcessResult
                    {
                        Success = false,
                        Message = "Ev sahibi ve deplasman takımları aynı olamaz."
                    };
                }

                var match = new Match(
                    matchDto.SeasonId,
                    matchDto.HomeTeamId,
                    matchDto.AwayTeamId,
                    matchDto.StadiumId,
                    matchDto.MatchDate
                );

                if (matchDto.HomeTeamScore.HasValue && matchDto.AwayTeamScore.HasValue)
                {
                    match.UpdateScore(matchDto.HomeTeamScore.Value, matchDto.AwayTeamScore.Value);
                }

                //if (Enum.TryParse<MatchStatus>(matchDto.Status, out var status))
                //{
                //    match.Status = status;
                //}

                return await _matchRepository.AddAsync(match);
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Maç eklenirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> UpdateMatchAsync(UpdateMatchDto matchDto)
        {
            try
            {
                var match = await _matchRepository.GetByIdAsync(matchDto.Id);
                if (match == null)
                {
                    return new ProcessResult
                    {
                        Success = false,
                        Message = "Maç bulunamadı."
                    };
                }

                if (matchDto.HomeTeamId == matchDto.AwayTeamId)
                {
                    return new ProcessResult
                    {
                        Success = false,
                        Message = "Ev sahibi ve deplasman takımları aynı olamaz."
                    };
                }

                _mapper.Map(matchDto, match);

                if (matchDto.HomeTeamScore.HasValue && matchDto.AwayTeamScore.HasValue)
                {
                    match.UpdateScore(matchDto.HomeTeamScore.Value, matchDto.AwayTeamScore.Value);
                }

                //if (Enum.TryParse<MatchStatus>(matchDto.Status, out var status))
                //{
                //    match.Status = status;
                //}

                return await _matchRepository.UpdateAsync(match);
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Maç güncellenirken hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> DeleteMatchAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Maç bulunamadı."
                };
            }

            return await _matchRepository.DeleteAsync(match);
        }

        public async Task<IEnumerable<MatchDto>> GetAllMatchesAsync()
        {
            var matches = await _matchRepository.GetAllWithRelationsAsync();
            return _mapper.Map<IEnumerable<MatchDto>>(matches);
        }

        public async Task<MatchDto> GetMatchByIdAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            return _mapper.Map<MatchDto>(match);
        }
    }
}
