using AutoMapper;
using FutScore.Application.DTOs.Stadium;
using FutScore.Application.Interfaces;
using FutScore.Domain;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;

namespace FutScore.Application.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;
        private readonly IMapper _mapper;

        public StadiumService(IStadiumRepository stadiumRepository, IMapper mapper)
        {
            _stadiumRepository = stadiumRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult> AddStadiumAsync(StadiumDto stadiumDto)
        {
            var stadium = _mapper.Map<Stadium>(stadiumDto);
            var result = await _stadiumRepository.AddAsync(stadium);
            return result;
        }

        public async Task<ProcessResult> UpdateStadiumAsync(StadiumDto stadiumDto)
        {
            var existingStadium = await _stadiumRepository.GetByIdAsync(stadiumDto.Id);
            if (existingStadium == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Stadyum bulunamadı."
                };
            }

            _mapper.Map(stadiumDto, existingStadium);
            return await _stadiumRepository.UpdateAsync(existingStadium);
        }

        public async Task<ProcessResult> DeleteStadiumAsync(int id)
        {
            var stadium = await _stadiumRepository.GetByIdAsync(id);
            if (stadium == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Stadyum bulunamadı."
                };
            }

            return await _stadiumRepository.DeleteAsync(stadium);
        }

        public async Task<IEnumerable<StadiumDto>> GetAllStadiumsAsync()
        {
            var stadiums = await _stadiumRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StadiumDto>>(stadiums);
        }

        public async Task<StadiumDto> GetStadiumByIdAsync(int id)
        {
            var stadium = await _stadiumRepository.GetByIdAsync(id);
            return _mapper.Map<StadiumDto>(stadium);
        }
    }
}
