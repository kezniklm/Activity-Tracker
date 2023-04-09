using Project.BL.Facades.Interfaces;
using Project.BL.Mappers;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class UserProjectFacade :
    FacadeBase<UserProjectEntity, UserProjectListModel, UserProjectDetailModel,
        UserProjectEntityMapper>, IUserProjectFacade
{
    private readonly IUserProjectModelMapper mapper;
    public UserProjectFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IUserProjectModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
        mapper = modelMapper;
    }

    public override async Task<UserProjectDetailModel> SaveAsync(UserProjectDetailModel model)
    {
        UserProjectDetailModel result;

        GuardCollectionsAreNotSet(model);

        UserProjectEntity entity = mapper.MapToEntity(model, model.Id);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> repository = uow.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            UserProjectEntity updatedEntity = await repository.UpdateAsync(entity);
            result = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            UserProjectEntity insertedEntity = await repository.InsertAsync(entity);
            result = ModelMapper.MapToDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }
}
