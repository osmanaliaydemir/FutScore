using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FutScore.Infrastructure.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Match>> GetAllWithRelationsAsync()
        {
            return await _context.Matches
                .Include(m => m.Season)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Stadium)
                .ToListAsync();
        }
    }
}