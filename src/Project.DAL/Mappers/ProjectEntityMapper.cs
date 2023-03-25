// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Project.DAL.Entities;

namespace Project.DAL.Mappers;
public class ProjectEntityMapper : IEntityMapper<ProjectEntity>
{
    public void MapToExistingEntity(ProjectEntity existingEntity, ProjectEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.Name = newEntity.Name;
    }
}
