using FutScore.Domain.Entities;

namespace FutScore.Application.Interfaces
{
    public interface IPlayerService : IBaseService<Player>
    {
        Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId);
        Task<IEnumerable<Player>> GetPlayersByPositionAsync(string position);
        Task TransferPlayerAsync(int playerId, int newTeamId);
        Task<bool> IsJerseyNumberAvailableAsync(int teamId, int jerseyNumber, int? excludePlayerId = null);
    }
} 