using Project.DAL.Entities;

namespace Project.DAL.Mappers;

public class UserProjectEntityMapper : IEntityMapper<UserProjectEntity>
{
    public void MapToExistingEntity(UserProjectEntity existingEntity, UserProjectEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.ProjectId = newEntity.ProjectId;
        existingEntity.UserId = newEntity.UserId;
    }
}
