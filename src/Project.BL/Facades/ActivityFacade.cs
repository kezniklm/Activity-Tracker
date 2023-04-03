using Microsoft.EntityFrameworkCore;
using Project.BL.Facades.Interfaces;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.UnitOfWork;

namespace Project.BL.Facades;

public class ActivityFacade : FacadeBase<ActivityEntity, ActivityListModel, ActivityDetailModel, ActivityEntityMapper>,
    IActivityFacade
{
    public ActivityFacade(
        IUnitOfWorkFactory unitOfWorkFactory,
        IActivityModelMapper modelMapper)
        : base(unitOfWorkFactory, modelMapper)
    {
    }

    public virtual async Task<IEnumerable<ActivityListModel>> Filter(DateTime start, DateTime end)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= start).Where(i => i.End <= end)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }
}
