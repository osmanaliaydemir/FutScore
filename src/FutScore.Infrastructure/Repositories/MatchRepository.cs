using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;

namespace FutScore.Infrastructure.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        private readonly ApplicationDbContext _context;
        public MatchRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}