using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;

namespace FutScore.Infrastructure.Repositories
{
    public class PlayerRepository : BaseRepository<Player>, IPlayerRepository
    {
        public PlayerRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
