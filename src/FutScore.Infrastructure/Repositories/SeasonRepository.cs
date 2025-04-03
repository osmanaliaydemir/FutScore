using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutScore.Infrastructure.Repositories
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly ApplicationDbContext _context;

        public SeasonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Season>> GetAllAsync()
        {
            return await _context.Seasons.ToListAsync();
        }

        public async Task<Season> GetByIdAsync(int id)
        {
            return await _context.Seasons.FindAsync(id);
        }

        public async Task<Season> FindAsync(Expression<Func<Season, bool>> predicate)
        {
            return await _context.Seasons.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Season entity)
        {
            await _context.Seasons.AddAsync(entity);
        }

        public void Update(Season entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(Season entity)
        {
            _context.Seasons.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Seasons.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<Season> GetSeasonWithTeamsAsync(int seasonId)
        {
            return await _context.Seasons
                .Include(s => s.SeasonTeams)
                .FirstOrDefaultAsync(s => s.Id == seasonId);
        }

        public async Task<Season> GetSeasonWithMatchesAsync(int seasonId)
        {
            return await _context.Seasons
                .Include(s => s.Matches)
                .FirstOrDefaultAsync(s => s.Id == seasonId);
        }

        public async Task<Season> GetSeasonWithStandingsAsync(int seasonId)
        {
            return await _context.Seasons
                .Include(s => s.Standings)
                .FirstOrDefaultAsync(s => s.Id == seasonId);
        }

        public async Task<IEnumerable<Season>> GetSeasonsByLeagueAsync(int leagueId)
        {
            return await _context.Seasons
                .Where(s => s.LeagueId == leagueId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Season>> GetActiveSeasonsByLeagueAsync(int leagueId)
        {
            return await _context.Seasons
                .Where(s => s.LeagueId == leagueId && s.Status == SeasonStatus.Active)
                .ToListAsync();
        }

        public async Task<bool> HasOverlappingDatesAsync(int leagueId, DateTime startDate, DateTime endDate, int? excludeId = null)
        {
            var query = _context.Seasons
                .Where(s => s.LeagueId == leagueId &&
                           ((s.StartDate <= startDate && s.EndDate >= startDate) ||
                            (s.StartDate <= endDate && s.EndDate >= endDate) ||
                            (s.StartDate >= startDate && s.EndDate <= endDate)));

            if (excludeId.HasValue)
            {
                query = query.Where(s => s.Id != excludeId.Value);
            }

            return await query.AnyAsync();
        }
    }
} 