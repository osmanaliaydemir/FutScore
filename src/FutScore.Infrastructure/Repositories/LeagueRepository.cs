using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;

namespace FutScore.Infrastructure.Repositories
{
    public class LeagueRepository : BaseRepository<League>, ILeagueRepository
    {
        public LeagueRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
