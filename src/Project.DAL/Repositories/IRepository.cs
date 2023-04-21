using Project.DAL.Entities;

namespace Project.DAL.Repositories;
public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Get();
    Task<TEntity?> GetOneAsync(Guid entityId);
    void Delete(Guid entityId);
    ValueTask<bool> ExistsAsync(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
