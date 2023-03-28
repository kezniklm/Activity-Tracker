﻿using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel { Name = entity.Name, Surname = entity.Surname };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Name = entity.Name,
                Surname = entity.Surname
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new() { Name = model.Name, Surname = model.Surname };
}