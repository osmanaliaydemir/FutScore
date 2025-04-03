using FutScore.Application.DTOs.Season;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutScore.Domain;

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

        public async Task<ProcessResult> CreateSeasonAsync(CreateSeasonDto seasonDto)
        {
            var season = _mapper.Map<Season>(seasonDto);
            return await _seasonRepository.AddAsync(season);
        }

        public async Task<ProcessResult> UpdateSeasonAsync(UpdateSeasonDto seasonDto)
        {
            var existingSeason = await _seasonRepository.GetByIdAsync(seasonDto.Id);
            if (existingSeason == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Sezon bulunamadý."
                };
            }

            _mapper.Map(seasonDto, existingSeason);
            return await _seasonRepository.UpdateAsync(existingSeason);
        }

        public async Task<ProcessResult> DeleteSeasonAsync(int seasonId)
        {
            var season = await _seasonRepository.GetByIdAsync(seasonId);
            if (season == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Sezon bulunamadý."
                };
            }

            return await _seasonRepository.DeleteAsync(season);
        }

        public async Task<SeasonDto> GetByIdAsync(int id)
        {
            var seasons = await _seasonRepository.GetByIdAsync(id);
            return _mapper.Map<SeasonDto>(seasons);
        }
    }
}
