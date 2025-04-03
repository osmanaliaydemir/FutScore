using FutScore.Domain.Entities;
using FutScore.Domain.Interfaces;
using FutScore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace FutScore.Infrastructure.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly ApplicationDbContext _context;
        public StadiumRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync()
        {
            return await _context.Set<Stadium>()
                .Where(x => !x.IsDeleted)
                .OrderBy(x => x.Name)
                .ToListAsync();
        }

        public async Task<Stadium> GetByIdAsync(int id)
        {
            return await _context.Set<Stadium>()
                .FirstOrDefaultAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<Stadium> AddAsync(Stadium entity)
        {
            entity.CreatedAt = DateTime.UtcNow;
            entity.IsActive = true;
            await _context.Set<Stadium>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<Stadium> UpdateAsync(Stadium entity)
        {
            var existingEntity = await GetByIdAsync(entity.Id);
            if (existingEntity == null)
                return null;

            entity.UpdatedAt = DateTime.UtcNow;
            entity.CreatedAt = existingEntity.CreatedAt;
            entity.CreatedBy = existingEntity.CreatedBy;
            
            _context.Entry(existingEntity).CurrentValues.SetValues(entity);
            await _context.SaveChangesAsync();
            
            return entity;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
                return false;

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.Set<Stadium>()
                .AnyAsync(x => x.Id == id && !x.IsDeleted);
        }

        public async Task<IEnumerable<Stadium>> GetStadiumsByCity(string city)
        {
            return await _context.Set<Stadium>()
                .Where(s => s.City.ToLower() == city.ToLower() && !s.IsDeleted)
                .OrderBy(s => s.Name)
                .ToListAsync();
        }

        public async Task<IEnumerable<Stadium>> GetStadiumsByCapacityRange(int minCapacity, int maxCapacity)
        {
            return await _context.Set<Stadium>()
                .Where(s => s.Capacity >= minCapacity && s.Capacity <= maxCapacity && !s.IsDeleted)
                .OrderByDescending(s => s.Capacity)
                .ToListAsync();
        }

        public async Task<Stadium> GetStadiumWithMatches(int id)
        {
            return await _context.Set<Stadium>()
                .Include(s => s.Matches)
                    .ThenInclude(m => m.HomeTeam)
                .Include(s => s.Matches)
                    .ThenInclude(m => m.AwayTeam)
                .FirstOrDefaultAsync(s => s.Id == id && !s.IsDeleted);
        }

        public Task<Stadium> FindAsync(Expression<Func<Stadium, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        Task IBaseRepository<Stadium>.AddAsync(Stadium entity)
        {
            return AddAsync(entity);
        }

        public void Update(Stadium entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Stadium entity)
        {
            throw new NotImplementedException();
        }

        public Task SaveChangesAsync()
        {
            throw new NotImplementedException();
        }
    }
} 