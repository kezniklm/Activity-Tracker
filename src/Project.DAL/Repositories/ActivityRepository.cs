using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;
using Project.DAL.Mappers;

namespace Project.DAL.Repositories;

public class ActivityRepository : Repository<ActivityEntity>
{
    public ActivityRepository(DbContext dbContext, IEntityMapper<ActivityEntity> entityMapper)
        : base(dbContext, entityMapper)
    {
    }

    public override async Task<ActivityEntity?> GetOneAsync(Guid entityId)
    {
        return await DbSet
            .Include(i => i.User)
            .SingleOrDefaultAsync(i => i.Id == entityId);
    }

    public override async Task<ActivityEntity> UpdateAsync(ActivityEntity entity)
    {
        ActivityEntity existingEntity = await DbSet
            .Include(i => i.User)
            .SingleAsync(e => e.Id == entity.Id);
        EntityMapper.MapToExistingEntity(existingEntity, entity);
        return existingEntity;
    }
}
