using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp.Data
{
    public abstract class SqliteRepository<T, K>: IRepository<T, K> where T: class
    {
        protected SqliteRepository(LibraryDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();
        }
        
        public async Task AddAsync(T entity)
        {
            if (entity != null)
            {
                _entities.Add(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                _entities.AddRange(entities);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _entities.ToListAsync();
        }

        public async Task<T> GetAsync(K entityId)
        {
            return await _entities.FindAsync(entityId);
        }

        public async Task RemoveAsync(K entityId)
        {
            var entity = await _entities.FindAsync(entityId);
            if (entity != null)
            {
                _entities.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(K entityId, T entity)
        {
            if (entity != null)
            {
                var oldEntity = await _entities.FindAsync(entityId);

                if (oldEntity != null)
                {
                    _context.Entry(oldEntity).CurrentValues.SetValues(entity);
                    await _context.SaveChangesAsync();
                }
                
            }
        }
        
        protected readonly LibraryDbContext _context;
        protected readonly DbSet<T> _entities;
    }
}