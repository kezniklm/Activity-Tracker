using Project.DAL.Entities;

namespace Project.DAL.Mappers;
public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.ActivityType = newEntity.ActivityType;
        existingEntity.Description = newEntity.Description;
        existingEntity.Start = newEntity.Start;
        existingEntity.End = newEntity.End;
        existingEntity.UserId = newEntity.UserId;
        existingEntity.ProjectId = newEntity.ProjectId;
    }
}
