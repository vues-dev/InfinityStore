namespace InfinityStoreAdmin.Api.Infrastructure.Database;

public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
    Task<int> CountAsync();
    Task<IEnumerable<TEntity>> GetPaginatedListAsync(int pageNumber, int pageSize);
    IQueryable<TEntity> Query();
    Task<TEntity> GetAsync(Guid id, CancellationToken cancellationToken = default);
}