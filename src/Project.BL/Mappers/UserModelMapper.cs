using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    public override UserListModel MapToListModel(UserEntity? entity)
        => entity is null
            ? UserListModel.Empty
            : new UserListModel
            {
                Id = entity.Id, Name = entity.Name, Surname = entity.Surname, PhotoUrl = entity.PhotoUrl
            };

    public override UserDetailModel MapToDetailModel(UserEntity? entity)
        => entity is null
            ? UserDetailModel.Empty
            : new UserDetailModel
            {
                Id = entity.Id, Name = entity.Name, Surname = entity.Surname, PhotoUrl = entity.PhotoUrl
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new() { Id = model.Id, Name = model.Name, Surname = model.Surname, PhotoUrl = model.PhotoUrl };
}
