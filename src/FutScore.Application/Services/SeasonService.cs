using FutScore.Application.DTOs.Season;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;

namespace FutScore.Application.Services
{
    public class SeasonService : ISeasonService
    {
        private readonly ISeasonRepository _seasonRepository;
        private readonly IMapper _mapper;

        public SeasonService(ISeasonRepository seasonRepository, IMapper mapper)
        {
            _seasonRepository = seasonRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<SeasonDto>> GetAllSeasonsAsync()
        {
            var seasons = await _seasonRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<SeasonDto>>(seasons);
        }

        public async Task<SeasonDto> GetSeasonByIdAsync(int id)
        {
            var season = await _seasonRepository.GetByIdAsync(id);
            return _mapper.Map<SeasonDto>(season);
        }

        public async Task<SeasonDto> CreateSeasonAsync(CreateSeasonDto seasonDto)
        {
            var season = _mapper.Map<Season>(seasonDto);
            await _seasonRepository.AddAsync(season);
            await _seasonRepository.SaveChangesAsync();
            return _mapper.Map<SeasonDto>(season);
        }

        public async Task UpdateSeasonAsync(UpdateSeasonDto seasonDto)
        {
            var season = await _seasonRepository.GetByIdAsync(seasonDto.Id);
            if (season == null)
                throw new KeyNotFoundException($"Season with ID {seasonDto.Id} not found.");

            _mapper.Map(seasonDto, season);
            _seasonRepository.Update(season);
            await _seasonRepository.SaveChangesAsync();
        }

        public async Task DeleteSeasonAsync(int id)
        {
            var season = await _seasonRepository.GetByIdAsync(id);
            if (season == null)
                throw new KeyNotFoundException($"Season with ID {id} not found.");

            _seasonRepository.Delete(season);
            await _seasonRepository.SaveChangesAsync();
        }

        public async Task<Season> GetSeasonWithTeamsAsync(int seasonId)
        {
            return await _seasonRepository.GetSeasonWithTeamsAsync(seasonId);
        }

        public async Task<Season> GetSeasonWithMatchesAsync(int seasonId)
        {
            return await _seasonRepository.GetSeasonWithMatchesAsync(seasonId);
        }

        public async Task<Season> GetSeasonWithStandingsAsync(int seasonId)
        {
            return await _seasonRepository.GetSeasonWithStandingsAsync(seasonId);
        }

        public async Task<IEnumerable<Season>> GetSeasonsByLeagueAsync(int leagueId)
        {
            return await _seasonRepository.GetSeasonsByLeagueAsync(leagueId);
        }

        public async Task<IEnumerable<Season>> GetActiveSeasonsByLeagueAsync(int leagueId)
        {
            return await _seasonRepository.GetActiveSeasonsByLeagueAsync(leagueId);
        }

        public async Task<bool> HasOverlappingDatesAsync(int leagueId, DateTime startDate, DateTime endDate, int? excludeId = null)
        {
            return await _seasonRepository.HasOverlappingDatesAsync(leagueId, startDate, endDate, excludeId);
        }
    }
} 