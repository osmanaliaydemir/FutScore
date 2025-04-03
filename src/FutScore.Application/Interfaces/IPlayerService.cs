using FutScore.Application.DTOs.Player;
using FutScore.Domain;

namespace FutScore.Application.Interfaces
{
    public interface IPlayerService
    {
        Task<ProcessResult> AddPlayerAsync(PlayerDto playerDto);
        Task<ProcessResult> UpdatePlayerAsync(PlayerDto playerDto);
        Task<ProcessResult> DeletePlayerAsync(int id);
        Task<IEnumerable<PlayerDto>> GetAllPlayersAsync();
        Task<PlayerDto> GetPlayerByIdAsync(int id);
    }
}
