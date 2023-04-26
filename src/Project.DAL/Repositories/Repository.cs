using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using Project.DAL.Mappers;

namespace Project.DAL.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    protected readonly DbSet<TEntity> DbSet;
    protected readonly IEntityMapper<TEntity> EntityMapper;

    public Repository(
        DbContext dbContext,
        IEntityMapper<TEntity> entityMapper)
    {
        DbSet = dbContext.Set<TEntity>();
        EntityMapper = entityMapper;
    }

    public virtual IQueryable<TEntity> Get() => DbSet;

    public virtual async Task<TEntity?> GetOneAsync(Guid entityId)
        => await DbSet.SingleOrDefaultAsync(i => i.Id == entityId);

    public virtual async ValueTask<bool> ExistsAsync(TEntity entity)
        => entity.Id != Guid.Empty && await DbSet.AnyAsync(e => e.Id == entity.Id);

    public virtual async Task<TEntity> InsertAsync(TEntity entity)
        => (await DbSet.AddAsync(entity)).Entity;

    public virtual async Task<TEntity> UpdateAsync(TEntity entity)
    {
        TEntity existingEntity = await DbSet.SingleAsync(e => e.Id == entity.Id);
        EntityMapper.MapToExistingEntity(existingEntity, entity);
        return existingEntity;
    }

    public virtual void Delete(Guid entityId) => DbSet.Remove(DbSet.Single(i => i.Id == entityId));
}
