using Microsoft.EntityFrameworkCore;
using Project.BL.Facades.Interfaces;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserProjectFacade :
    FacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel,
        UserProjectEntityMapper>, IUserProjectFacade
{
    protected readonly IModelMapperBase<ProjectEntity, ProjectListModel, ProjectDetailModel> ProjectMapper;

    public UserProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper modelMapper, IProjectModelMapper projectMapper)
        : base(unitOfWorkFactory, modelMapper) =>
        ProjectMapper = projectMapper;

    public async Task<UserProjectDetailModel?> GetUserProjectByIds(Guid userId, Guid projectId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<UserProjectEntity> query = uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>().Get();
        UserProjectEntity? entity = await query.SingleOrDefaultAsync(e => e.UserId == userId && e.ProjectId == projectId);

        return entity is null
            ? null
            : ModelMapper.MapToDetailModel(entity);

    }

    public async Task<Tuple<IEnumerable<ProjectListModel>, IEnumerable<ProjectListModel>>>
        DisplayProjectsOfUser(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<UserProjectEntity> entities = await uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>().Get()
            .Where(e => e.UserId == userId).ToListAsync();
        List<ProjectListModel> result1 = new();

        foreach (UserProjectEntity entity in entities)
        {
            ProjectEntity? projectEntity =
                await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().GetOneAsync(entity.ProjectId);

            result1.Add(ProjectMapper.MapToListModel(projectEntity));
        }

        List<ProjectEntity> projects = await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get().ToListAsync();
        List<ProjectListModel> result2 = new();

        foreach (ProjectEntity project in projects)
        {
            bool inProject = false;
            foreach (UserProjectEntity user in project.Users )
            {
                if (user.UserId == userId)
                {
                    inProject = true;
                }
            }

            if (!inProject)
            {
                result2.Add(ProjectMapper.MapToListModel(project));
            }
            
        }

        return Tuple.Create(result1.AsEnumerable(), result2.AsEnumerable());
    }

    public async Task<IEnumerable<ProjectListModel>?> DisplayOtherProjectsForUser(Guid userId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ProjectEntity> entities = await uow.GetRepository<ProjectEntity, ProjectEntityMapper>().Get().ToListAsync();
        List<ProjectListModel> result = new();

        foreach (ProjectEntity entity in entities)
        {
            result.Add(ProjectMapper.MapToListModel(entity));
        }

        return result.AsEnumerable();
    }
}
