using Microsoft.EntityFrameworkCore;

namespace SimpleMoq.Api.Common;

public interface IRepository<T, K> where T : IEntity<K>
{
    public Task<List<T>> GetAll();
    public Task<T?> GetByIdAsync(K id, bool track = true);

    public Task<T> CreateAsync(T entity);
    public Task<T> DeleteAsync(T entity);
    public Task<T> UpdateAsync(T entity);
}

public class RepositoryBase<T, K> : IRepository<T, K>
    where T : class, IEntity<K>
{
    protected readonly DbContext Context;

    protected RepositoryBase(DbContext context)
    {
        Context = context;
    }


    public virtual Task<List<T>> GetAll()
    {
        return Context
            .Set<T>()
            .AsQueryable()
            .ToListAsync();
    }

    public virtual async Task<T?> GetByIdAsync(K id, bool track = true)
    {
        if (track)
        {
            return await Context
                .Set<T>()
                .AsTracking()
                .SingleOrDefaultAsync(x => x.Id!.Equals(id));
        }

        return await Context
            .Set<T>()
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id!.Equals(id));
    }

    public async Task<T> CreateAsync(T entity)
    {
        var entry = await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();

        return entry.Entity;
    }

    public async Task<T> DeleteAsync(T entity)
    {
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }
}
