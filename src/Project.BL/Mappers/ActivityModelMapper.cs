using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class ActivityModelMapper : ModelMapperBase<ActivityEntity, ActivityListModel, ActivityDetailModel>,
    IActivityModelMapper
{
    public override ActivityListModel MapToListModel(ActivityEntity? entity)
        => entity is null
            ? ActivityListModel.Empty
            : new ActivityListModel { Id = entity.Id, ActivityType = entity.ActivityType, Start = entity.Start, End = entity.End };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity?.User is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                ActivityType = entity.ActivityType,
                Start = entity.Start,
                End = entity.End,
                UserId = entity.User.Id,
                UserName = entity.User.Name,
                UserSurname = entity.User.Surname,
                Description = entity.Description,
                ProjectId = entity.ProjectId
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new()
        {
            Id = model.Id, ActivityType = model.ActivityType, Start = model.Start, End = model.End, UserId = model.UserId, Description = model.Description, ProjectId = model.ProjectId, User = new(){Id = model.UserId, Name = model.UserName, Surname = model.UserSurname}
        };
}
