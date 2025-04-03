using FutScore.Domain.Interfaces;
using FutScore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FutScore.Infrastructure.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly DbContext _context;
        private readonly DbSet<T> _dbSet;

        public BaseRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }

        public async Task<ProcessResult> AddAsync(T entity)
        {
            try
            {
                await _dbSet.AddAsync(entity);
                return await SaveChangesWithResultAsync(entity);
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Ekleme işlemi sırasında hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> UpdateAsync(T entity)
        {
            try
            {
                _dbSet.Update(entity);
                return await SaveChangesWithResultAsync(entity);
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Güncelleme sırasında hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> DeleteAsync(T entity)
        {
            try
            {
                if (typeof(T).GetProperty("IsDeleted") != null)
                {
                    entity.GetType().GetProperty("IsDeleted").SetValue(entity, true);
                    return await UpdateAsync(entity);
                }

                _dbSet.Remove(entity);
                return await SaveChangesWithResultAsync(entity);
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Silme işlemi sırasında hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> ExistsAsync(int id)
        {
            try
            {
                bool exists = await _dbSet.FindAsync(id) != null;
                return new ProcessResult
                {
                    Success = exists,
                    Message = exists ? "Kayıt bulundu." : "Kayıt bulunamadı.",
                    EntityId = exists ? id : null
                };
            }
            catch (Exception ex)
            {
                return new ProcessResult
                {
                    Success = false,
                    Message = $"Varlık kontrolü sırasında hata oluştu: {ex.Message}"
                };
            }
        }

        public async Task<ProcessResult> SaveChangesAsync()
        {
            return await SaveChangesWithResultAsync();
        }

        private async Task<ProcessResult> SaveChangesWithResultAsync(object entityId = null)
        {
            var processResult = new ProcessResult();

            try
            {
                int affectedRows = await _context.SaveChangesAsync();
                processResult.Success = affectedRows > 0;
                processResult.Message = affectedRows > 0 ? "İşlem başarılı." : "Herhangi bir değişiklik yapılmadı.";
                processResult.EntityId = entityId;
            }
            catch (Exception ex)
            {
                processResult.Success = false;
                processResult.Message = $"Veritabanına kayıt işlemi sırasında hata oluştu: {ex.Message}";
            }

            return processResult;
        }
    }

}
