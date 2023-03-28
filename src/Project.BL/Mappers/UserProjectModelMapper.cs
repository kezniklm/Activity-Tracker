using System;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.BL.Mappers;

namespace CookBook.BL.Mappers;

public class UserProjectModelMapper :
    ModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>,
    IUserProjectModelMapper
{
    public override UserProjectListModel MapToListModel(UserProjectEntity? entity)
        => entity?.User is null
            ? UserProjectListModel.Empty
            : new UserProjectListModel
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                UserId = entity.UserId
            };

    public override UserProjectDetailModel MapToDetailModel(UserProjectEntity? entity)
        => entity?.User is null
            ? UserProjectDetailModel.Empty
            : new UserProjectDetailModel
            {
                Id = entity.Id,
                ProjectId = entity.ProjectId,
                UserId = entity.UserId
            };

    public UserProjectListModel MapToListModel(UserProjectDetailModel detailModel)
        => new()
        {
            Id = detailModel.Id,
            ProjectId = detailModel.ProjectId,
            UserId = detailModel.UserId
        };

    public void MapToExistingDetailModel(UserProjectDetailModel existingDetailModel,
        UserProjectListModel userProjects)
    {
        existingDetailModel.Id = userProjects.Id;
        existingDetailModel.ProjectId = userProjects.ProjectId;
        existingDetailModel.UserId = userProjects.UserId;
    }

    public override UserProjectEntity MapToEntity(UserProjectDetailModel model)
        => throw new NotImplementedException("This method is unsupported. Use the other overload.");


    public UserProjectEntity MapToEntity(UserProjectDetailModel model, Guid id)
        => new()
        {
            Id = model.Id,
            ProjectId = model.ProjectId,
            UserId = model.UserId
        };

    public UserProjectEntity MapToEntity(UserProjectListModel model, Guid id)
        => new()
        {
            Id = model.Id,
            ProjectId = model.ProjectId,
            UserId = model.UserId
        };
}
