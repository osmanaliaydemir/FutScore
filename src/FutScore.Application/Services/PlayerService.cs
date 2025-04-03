using FutScore.Application.DTOs.Player;
using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using FutScore.Domain;

namespace FutScore.Application.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public PlayerService(IPlayerRepository playerRepository, IMapper mapper)
        {
            _playerRepository = playerRepository;
            _mapper = mapper;
        }

        public async Task<ProcessResult> AddPlayerAsync(PlayerDto playerDto)
        {
            var player = _mapper.Map<Player>(playerDto);
            var result = await _playerRepository.AddAsync(player);
            return result;
        }

        public async Task<ProcessResult> UpdatePlayerAsync(PlayerDto playerDto)
        {
            var existingPlayer = await _playerRepository.GetByIdAsync(playerDto.Id);
            if (existingPlayer == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Oyuncu bulunamadý."
                };
            }

            _mapper.Map(playerDto, existingPlayer);
            return await _playerRepository.UpdateAsync(existingPlayer);
        }

        public async Task<ProcessResult> DeletePlayerAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            if (player == null)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = "Oyuncu bulunamadý."
                };
            }

            return await _playerRepository.DeleteAsync(player);
        }

        public async Task<IEnumerable<PlayerDto>> GetAllPlayersAsync()
        {
            var players = await _playerRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<PlayerDto>>(players);
        }

        public async Task<PlayerDto> GetPlayerByIdAsync(int id)
        {
            var player = await _playerRepository.GetByIdAsync(id);
            return _mapper.Map<PlayerDto>(player);
        }
    }
}
