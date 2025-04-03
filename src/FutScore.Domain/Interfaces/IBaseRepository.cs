using System.Linq.Expressions;

namespace FutScore.Domain.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
        Task<ProcessResult> AddAsync(T entity);
        Task<ProcessResult> UpdateAsync(T entity);
        Task<ProcessResult> DeleteAsync(T entity);
        Task<ProcessResult> ExistsAsync(int id);
        Task<ProcessResult> SaveChangesAsync();
    }

}