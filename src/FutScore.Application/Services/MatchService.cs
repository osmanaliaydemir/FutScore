using FutScore.Application.DTOs.Match;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Linq.Expressions;

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

        public async Task<MatchDto> CreateMatchAsync(CreateMatchDto matchDto)
        {
            var match = _mapper.Map<Match>(matchDto);
            await _matchRepository.AddAsync(match);
            await _matchRepository.SaveChangesAsync();
            return _mapper.Map<MatchDto>(match);
        }

        public async Task UpdateMatchAsync(UpdateMatchDto matchDto)
        {
            var match = await _matchRepository.GetByIdAsync(matchDto.Id);
            if (match == null)
                throw new KeyNotFoundException($"Match with ID {matchDto.Id} not found.");

            _mapper.Map(matchDto, match);
            _matchRepository.Update(match);
            await _matchRepository.SaveChangesAsync();
        }

        public async Task DeleteMatchAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
                throw new KeyNotFoundException($"Match with ID {id} not found.");

            _matchRepository.Delete(match);
            await _matchRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Match>> GetMatchesByTeamAsync(int teamId)
        {
            return await _matchRepository.GetMatchesByTeamAsync(teamId);
        }

        public async Task<IEnumerable<Match>> GetUpcomingMatchesAsync(int? seasonId = null)
        {
            return await _matchRepository.GetUpcomingMatchesAsync(seasonId);
        }

        public async Task<IEnumerable<Match>> GetCompletedMatchesAsync(int? seasonId = null)
        {
            return await _matchRepository.GetCompletedMatchesAsync(seasonId);
        }

        public async Task UpdateMatchScoreAsync(int matchId, int homeScore, int awayScore)
        {
            await _matchRepository.UpdateMatchScoreAsync(matchId, homeScore, awayScore);
        }

        public async Task FinishMatchAsync(int matchId)
        {
            await _matchRepository.FinishMatchAsync(matchId);
        }

        public async Task<bool> IsMatchTimeValidAsync(int seasonId, DateTime matchDate)
        {
            return await _matchRepository.IsMatchTimeValidAsync(seasonId, matchDate);
        }

        public async Task<bool> AreTeamsAvailableAsync(int seasonId, int homeTeamId, int awayTeamId, DateTime matchDate)
        {
            return await _matchRepository.AreTeamsAvailableAsync(seasonId, homeTeamId, awayTeamId, matchDate);
        }

        public async Task<Match> AddAsync(Match entity)
        {
            await _matchRepository.AddAsync(entity);
            await _matchRepository.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(Match entity)
        {
            _matchRepository.Update(entity);
            await _matchRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var match = await _matchRepository.GetByIdAsync(id);
            if (match == null)
                throw new KeyNotFoundException($"Match with ID {id} not found.");

            _matchRepository.Delete(match);
            await _matchRepository.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _matchRepository.ExistsAsync(id);
        }

        public async Task<IEnumerable<Match>> GetAllAsync()
        {
            return await _matchRepository.GetAllAsync();
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            return await _matchRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Match>> FindAsync(Expression<Func<Match, bool>> predicate)
        {
            var result = await _matchRepository.FindAsync(predicate);
            return result != null ? new List<Match> { result } : Enumerable.Empty<Match>();
        }
    }
} 