using Microsoft.EntityFrameworkCore;

namespace InfinityStoreAdmin.Api.Infrastructure.Database;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly DatabaseContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(DatabaseContext context, DbSet<TEntity> dbSet)
    {
        _context = context;
        _dbSet = dbSet;
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        var entityEntry = await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Update(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<TEntity> DeleteAsync(TEntity entity)
    {
        var entityEntry = _dbSet.Remove(entity);
        await _context.SaveChangesAsync();

        return entityEntry.Entity;
    }

    public async Task<int> CountAsync()
    {
        return await _dbSet.CountAsync();
    }

    public async Task<IEnumerable<TEntity>> GetPaginatedListAsync(int pageNumber, int pageSize)
    {
        return await _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public IQueryable<TEntity> Query()
    {
        return _dbSet;
    }

    public async Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(id, cancellationToken);
    }

}