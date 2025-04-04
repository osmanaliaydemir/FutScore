using FutScore.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FutScore.Domain.Interfaces
{
    public interface IMatchRepository : IBaseRepository<Match>
    {
        Task<IEnumerable<Match>> GetAllWithRelationsAsync();
    }
} 