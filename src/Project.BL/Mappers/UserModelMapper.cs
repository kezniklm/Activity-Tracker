using CommunityToolkit.Maui.Core.Extensions;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class UserModelMapper : ModelMapperBase<UserEntity, UserListModel, UserDetailModel>,
    IUserModelMapper
{
    private readonly IActivityModelMapper _activityModelMapper;
    private readonly IUserProjectModelMapper _userProjectModelMapper;

    public UserModelMapper(IUserProjectModelMapper userProjectModelMapper, IActivityModelMapper activityModelMapper)
    {
        _userProjectModelMapper = userProjectModelMapper;
        _activityModelMapper = activityModelMapper;
    }

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
                Id = entity.Id,
                Name = entity.Name,
                Surname = entity.Surname,
                PhotoUrl = entity.PhotoUrl,
                Projects = _userProjectModelMapper.MapToListModel(entity.Projects)
                    .ToObservableCollection(),
                Activities = _activityModelMapper.MapToEnumerableList(entity.Activities)
                    .ToObservableCollection()
            };

    public override UserEntity MapToEntity(UserDetailModel model)
        => new() { Id = model.Id, Name = model.Name, Surname = model.Surname, PhotoUrl = model.PhotoUrl };

    public IEnumerable<UserListModel> MapToEnumerableList(ICollection<UserEntity> users)
    {
        IEnumerable<UserListModel> result = new List<UserListModel>();
        foreach (UserEntity user in users)
        {
            UserListModel userListModel = new()
            {
                Name = user.Name, Surname = user.Surname, PhotoUrl = user.PhotoUrl, Id = user.Id
            };
            result = result.Append(userListModel);
        }

        return result;
    }
}
