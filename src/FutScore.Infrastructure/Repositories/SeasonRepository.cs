using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Infrastructure.Repositories
{
    public class SeasonRepository : BaseRepository<Season>, ISeasonRepository
    {
        private readonly ApplicationDbContext _context;

        public SeasonRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Season>> GetAllWithLeaguesAsync()
        {
            return await _context.Seasons
                .Include(s => s.League)
                .ToListAsync();
        }
    }
}