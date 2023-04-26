using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class UserProjectModelMapper :
    ModelMapperBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel>,
    IUserProjectModelMapper
{
    public override UserProjectListModel MapToListModel(UserProjectEntity? entity)
        => entity?.User is null
            ? UserProjectListModel.Empty
            : new UserProjectListModel { Id = entity.Id, ProjectId = entity.ProjectId, UserId = entity.UserId };

    public override UserProjectDetailModel MapToDetailModel(UserProjectEntity? entity)
        => entity is null
            ? UserProjectDetailModel.Empty
            : new UserProjectDetailModel { Id = entity.Id, ProjectId = entity.ProjectId, UserId = entity.UserId };

    public override UserProjectEntity MapToEntity(UserProjectDetailModel model)
        => new() { Id = model.Id, ProjectId = model.ProjectId, UserId = model.UserId };
}
