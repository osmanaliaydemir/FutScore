using FutScore.Application.Interfaces;
using FutScore.Domain.Entities;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Application.Services
{
    public class PlayerService : BaseService<Player>, IPlayerService
    {
        public PlayerService(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Player>> GetPlayersByTeamAsync(int teamId)
        {
            return await _dbSet
                .Where(p => p.TeamId == teamId)
                .OrderBy(p => p.JerseyNumber)
                .ToListAsync();
        }

        public async Task<IEnumerable<Player>> GetPlayersByPositionAsync(string position)
        {
            return await _dbSet
                .Where(p => p.Position == position)
                .Include(p => p.Team)
                .OrderBy(p => p.Team.Name)
                .ThenBy(p => p.JerseyNumber)
                .ToListAsync();
        }

        public async Task TransferPlayerAsync(int playerId, int newTeamId)
        {
            var player = await _dbSet.FindAsync(playerId);
            if (player == null)
                throw new ArgumentException("Player not found", nameof(playerId));

            var newTeam = await _context.Teams.FindAsync(newTeamId);
            if (newTeam == null)
                throw new ArgumentException("Team not found", nameof(newTeamId));

            player.TeamId = newTeamId;
            player.JerseyNumber = null; // Reset jersey number as it might conflict in new team

            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsJerseyNumberAvailableAsync(int teamId, int jerseyNumber, int? excludePlayerId = null)
        {
            var query = _dbSet.Where(p => p.TeamId == teamId && p.JerseyNumber == jerseyNumber);

            if (excludePlayerId.HasValue)
            {
                query = query.Where(p => p.Id != excludePlayerId.Value);
            }

            return !await query.AnyAsync();
        }
    }
} 