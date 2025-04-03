using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;

namespace FutScore.Infrastructure.Repositories
{
    public class StadiumRepository : BaseRepository<Stadium>, IStadiumRepository
    {
        private readonly ApplicationDbContext _context;
        public StadiumRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}