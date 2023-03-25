// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

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
