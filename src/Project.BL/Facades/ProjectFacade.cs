using Project.BL.Facades.Interfaces;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class ProjectFacade : FacadeBase<ProjectEntity, ProjectListModel, ProjectDetailModel, ProjectEntityMapper>,
    IProjectFacade
{
    public ProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public override async Task<ProjectDetailModel> SaveAsync(ProjectDetailModel model)
    {
        ProjectDetailModel result;

        //GuardCollectionsAreNotSet(model);
        GuardSameName(model);

        ProjectEntity entity = ModelMapper.MapToEntity(model);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectEntity> repository = uow.GetRepository<ProjectEntity, ProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            ProjectEntity updatedEntity = await repository.UpdateAsync(entity);
            result = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            ProjectEntity insertedEntity = await repository.InsertAsync(entity);
            result = ModelMapper.MapToDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }

    public void GuardSameName(ProjectDetailModel saveProjectDetail)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ProjectEntity> repository = uow.GetRepository<ProjectEntity, ProjectEntityMapper>();
        IQueryable<ProjectEntity> projects = repository.Get();

        foreach (ProjectEntity project in projects)
        {
            if (project.Name == saveProjectDetail.Name && project.Id != saveProjectDetail.Id)
            {
                throw new InvalidOperationException("Two projects can not have the same name!");
            }
        }
    }
}
