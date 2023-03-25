// Copyright (c) .NET Foundation and contributors. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.
//

using Project.DAL.Entities;

namespace Project.DAL.Mappers;
public class UserEntityMapper : IEntityMapper<UserEntity>
{
    public void MapToExistingEntity(UserEntity existingEntity, UserEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.Name = newEntity.Name;
        existingEntity.Surname = newEntity.Surname;
        existingEntity.PhotoUrl = newEntity.PhotoUrl;
    }
}
