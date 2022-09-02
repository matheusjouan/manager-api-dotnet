using Manager.Core.Entities;

namespace Manager.Core.Interfaces;

public interface IBaseRepository<T> where T : EntityBase
{
    Task<List<T>> GetAllAsync();
    Task<T> GetByIdAsync(long id);
    Task<T> CreateAsync(T obj);
    Task<T> UpdateAsync(T obj);
    Task RemoveAsync(T obj);
}

