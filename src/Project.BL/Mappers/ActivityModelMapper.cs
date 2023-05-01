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
            : new ActivityListModel
            {
                Id = entity.Id, ActivityType = entity.ActivityType, Start = entity.Start, End = entity.End
            };

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

    public override ActivityEntity MapToEntity(ActivityDetailModel model) => throw new NotImplementedException();

    public IEnumerable<ActivityListModel> MapToEnumerableList(ICollection<ActivityEntity> activities)
    {
        IEnumerable<ActivityListModel> result = new List<ActivityListModel>();
        foreach (ActivityEntity activity in activities)
        {
            ActivityListModel activityListModel = new()
            {
                ActivityType = activity.ActivityType, Start = activity.Start, End = activity.End, Id = activity.Id
            };
            result = result.Append(activityListModel);
        }

        return result;
    }

    public static ActivityEntity MapToActivityEntity(ActivityDetailModel model, UserEntity? user)
        => new()
        {
            Id = model.Id,
            ActivityType = model.ActivityType,
            Start = model.Start,
            End = model.End,
            UserId = model.UserId,
            Description = model.Description,
            ProjectId = model.ProjectId,
            User = user
        };
}
