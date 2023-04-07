using Microsoft.EntityFrameworkCore;
using Project.BL.Facades.Interfaces;
using Project.BL.Mappers.Interfaces;
using Project.BL.Models;
using Project.DAL.Entities;
using Project.DAL.Mappers;
using Project.DAL.Repositories;
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

    public override async Task<ActivityDetailModel> SaveAsync(ActivityDetailModel model)
    {
        ActivityDetailModel result;

        GuardDateTimeCorrect(model.Start, model.End);
        GuardDateTimeIsNotSame(model);

        ActivityEntity entity = ModelMapper.MapToEntity(model);

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            ActivityEntity updatedEntity = await repository.UpdateAsync(entity);
            result = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            ActivityEntity insertedEntity = await repository.InsertAsync(entity);
            result = ModelMapper.MapToDetailModel(insertedEntity);
        }

        await uow.CommitAsync();

        return result;
    }

    public async Task<IEnumerable<ActivityListModel>> Filter(DateTime start, DateTime end)
    {
        GuardDateTimeCorrect(start, end);
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= start).Where(i => i.End <= end)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisYear()
    {
        int year = DateTime.Today.Year;
        DateTime thisYear = new (year, 1, 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= thisYear).Where(i => i.Start <= DateTime.Today )
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterLastMonth()
    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        DateTime lastMonthStart = new(year, month - 1, 1);
        DateTime nextMonthStart = new(year, month, 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= lastMonthStart).Where(i => i.Start < nextMonthStart)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisMonth()
    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        DateTime thisMonthStart = new(year, month, 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= thisMonthStart).Where(i => i.Start <= DateTime.Today)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisWeek()
    {
        DateTime today = DateTime.Today;
        int day = (int)today.DayOfWeek;
        DateTime thisWeek = today.AddDays(- day + 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= thisWeek).Where(i => i.Start <= today)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public void GuardDateTimeIsNotSame(ActivityDetailModel model)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        var activities = repository.Get();

        foreach (var activity in activities)
        {
            if (activity.Id != model.Id)
            {
                if ((activity.Start <= model.Start && activity.End >= model.End) ||
                    (activity.Start >= model.Start && activity.End <= model.End) ||
                    (activity.Start > model.Start && activity.Start < model.End) ||
                    (activity.End > model.Start && activity.End < model.End))
                {
                    throw new InvalidOperationException("User cannot have two activities in the same time period.");
                }
            }
        }
    }

    public static void GuardDateTimeCorrect(DateTime start, DateTime end)
    {
        if (start >= end)
        {
            throw new InvalidOperationException("Activity start cannot be greater than end.");
        }
    }
}
