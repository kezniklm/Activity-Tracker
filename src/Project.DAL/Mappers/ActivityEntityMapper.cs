// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Project.DAL.Entities;

namespace Project.DAL.Mappers;
public class ActivityEntityMapper : IEntityMapper<ActivityEntity>
{
    public void MapToExistingEntity(ActivityEntity existingEntity, ActivityEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.ActivityType= newEntity.ActivityType;
        existingEntity.Description= newEntity.Description;
        existingEntity.Start= newEntity.Start;
        existingEntity.End= newEntity.End;
        existingEntity.UserId= newEntity.UserId;
        existingEntity.ProjectId= newEntity.ProjectId;
    }
}
