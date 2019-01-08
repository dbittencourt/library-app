using System.Collections.Generic;
using System.Threading.Tasks;

namespace LibraryApp.Data
{
    public interface IRepository<T, K> where T: class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(K entityId);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(K entityId, T entity);
        Task RemoveAsync(K entityId);
    }
}