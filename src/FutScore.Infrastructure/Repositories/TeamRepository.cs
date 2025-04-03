using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutScore.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly ApplicationDbContext _context;

        public TeamRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetByIdAsync(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task<Team> FindAsync(Expression<Func<Team, bool>> predicate)
        {
            return await _context.Teams.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Team entity)
        {
            await _context.Teams.AddAsync(entity);
        }

        public void Update(Team entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Team entity)
        {
            _context.Teams.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Teams.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Team> GetTeamWithPlayersAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.Players)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<Team> GetTeamWithMatchesAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.HomeMatches)
                .Include(t => t.AwayMatches)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<Team> GetTeamWithSeasonsAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.SeasonTeams)
                .ThenInclude(st => st.Season)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<Team> GetTeamWithStandingsAsync(int teamId)
        {
            return await _context.Teams
                .Include(t => t.Standings)
                .FirstOrDefaultAsync(t => t.Id == teamId);
        }

        public async Task<IEnumerable<Team>> GetTeamsBySeasonAsync(int seasonId)
        {
            return await _context.Teams
                .Where(t => t.SeasonTeams.Any(st => st.SeasonId == seasonId))
                .ToListAsync();
        }

        public async Task<bool> IsTeamInSeasonAsync(int teamId, int seasonId)
        {
            return await _context.SeasonTeams
                .AnyAsync(st => st.TeamId == teamId && st.SeasonId == seasonId);
        }

        public async Task AddTeamToSeasonAsync(int teamId, int seasonId)
        {
            var seasonTeam = new SeasonTeam
            {
                TeamId = teamId,
                SeasonId = seasonId
            };

            await _context.SeasonTeams.AddAsync(seasonTeam);
            await SaveChangesAsync();
        }

        public async Task RemoveTeamFromSeasonAsync(int teamId, int seasonId)
        {
            var seasonTeam = await _context.SeasonTeams
                .FirstOrDefaultAsync(st => st.TeamId == teamId && st.SeasonId == seasonId);

            if (seasonTeam != null)
            {
                _context.SeasonTeams.Remove(seasonTeam);
                await SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Team>> GetTeamsByLeagueAsync(int leagueId)
        {
            return await _context.Teams
                .Where(t => t.LeagueId == leagueId)
                .ToListAsync();
        }

        public async Task<bool> IsTeamNameUniqueAsync(string name, int? excludeId = null)
        {
            var query = _context.Teams.Where(t => t.Name == name);
            if (excludeId.HasValue)
            {
                query = query.Where(t => t.Id != excludeId.Value);
            }
            return !await query.AnyAsync();
        }
    }
} 