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
            : new ActivityListModel { ActivityType = entity.ActivityType, Start = entity.Start, End = entity.End };

    public override ActivityDetailModel MapToDetailModel(ActivityEntity? entity)
        => entity is null
            ? ActivityDetailModel.Empty
            : new ActivityDetailModel
            {
                Id = entity.Id,
                ActivityType = entity.ActivityType,
                Start = entity.Start,
                End = entity.End,
                User = entity.User
            };

    public override ActivityEntity MapToEntity(ActivityDetailModel model)
        => new() { Id = model.Id, ActivityType = model.ActivityType, Start = model.Start, End = model.End, User = model.User };
}
