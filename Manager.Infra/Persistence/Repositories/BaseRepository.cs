using Manager.Core.Entities;
using Manager.Core.Interfaces;
using Manager.Infra.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Manager.Infra.Persistence.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : EntityBase
{
    private readonly ManagerContext _context;

    public BaseRepository(ManagerContext context)
    {
        _context = context;
    }

    public async Task<T> CreateAsync(T obj)
    {
        _context.Set<T>().Add(obj);
        await _context.SaveChangesAsync();

        return obj;
    }

    public async Task<List<T>> GetAllAsync()
    {
        var users = await _context.Set<T>()
            .AsNoTracking()
            .ToListAsync();

        return users;
    }

    public async Task<T> GetByIdAsync(long id)
    {
        var user = await _context.Set<T>()
            .Where(x => x.Id == id)
            .AsNoTracking()
            .FirstOrDefaultAsync();

        return user;
    }

    public async Task RemoveAsync(T obj)
    {
        _context.Set<T>().Remove(obj);
        await _context.SaveChangesAsync();
    }

    public async Task<T> UpdateAsync(T obj)
    {
        _context.Entry(obj).State = EntityState.Modified;
        _context.Set<T>().Update(obj);

        await _context.SaveChangesAsync();

        return obj;
    }
}

