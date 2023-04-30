using CommunityToolkit.Maui.Core.Extensions;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;

namespace Project.BL.Mappers;

public class ProjectModelMapper : ModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel>,
    IProjectModelMapper
{
    private readonly IActivityModelMapper _activityMapper;
    private readonly IUserProjectModelMapper _userProjectMapper;

    public ProjectModelMapper(IActivityModelMapper activityMapper, IUserProjectModelMapper userProjectMapper)
    {
        _activityMapper = activityMapper;
        _userProjectMapper = userProjectMapper;
    }

    public override ProjectListModel MapToListModel(ProjectEntity? entity)
        => entity is null
            ? ProjectListModel.Empty
            : new ProjectListModel { Id = entity.Id, Name = entity.Name };

    public override ProjectDetailModel MapToDetailModel(ProjectEntity? entity)
        => entity is null
            ? ProjectDetailModel.Empty
            : new ProjectDetailModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Activities = _activityMapper.MapToEnumerableList(entity.Activities).ToObservableCollection(),
                Users = _userProjectMapper.MapToListModel(entity.Users).ToObservableCollection(),
            };

    public override ProjectEntity MapToEntity(ProjectDetailModel model)
        => new() { Id = model.Id, Name = model.Name };
}
