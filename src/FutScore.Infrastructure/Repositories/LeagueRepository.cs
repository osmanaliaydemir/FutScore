using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutScore.Infrastructure.Repositories
{
    public class LeagueRepository : ILeagueRepository
    {
        private readonly ApplicationDbContext _context;

        public LeagueRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<League>> GetAllAsync()
        {
            return await _context.Leagues.ToListAsync();
        }

        public async Task<League> GetByIdAsync(int id)
        {
            return await _context.Leagues.FindAsync(id);
        }

        public async Task<League> FindAsync(Expression<Func<League, bool>> predicate)
        {
            return await _context.Leagues.FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(League entity)
        {
            await _context.Leagues.AddAsync(entity);
        }

        public void Update(League entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(League entity)
        {
            _context.Leagues.Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Leagues.AnyAsync(e => e.Id == id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<League> GetLeagueWithSeasonsAsync(int leagueId)
        {
            return await _context.Leagues
                .Include(l => l.Seasons)
                .FirstOrDefaultAsync(l => l.Id == leagueId);
        }

        public async Task<IEnumerable<League>> GetLeaguesByCountryAsync(string country)
        {
            return await _context.Leagues
                .Where(l => l.Country == country)
                .ToListAsync();
        }

        public async Task<bool> IsLeagueNameUniqueAsync(string name, int? excludeId = null)
        {
            var query = _context.Leagues.Where(l => l.Name == name);
            if (excludeId.HasValue)
            {
                query = query.Where(l => l.Id != excludeId.Value);
            }
            return !await query.AnyAsync();
        }
    }
} 