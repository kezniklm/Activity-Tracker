using Microsoft.EntityFrameworkCore;
using Project.BL.Facades.Interfaces;
using Project.BL.Mappers;
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
        GuardTwoActivitiesNotAtTheSameTime(model);
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> activityRepository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        IRepository<UserEntity> userRepository = uow.GetRepository<UserEntity, UserEntityMapper>();

        UserEntity? user;
        user = await userRepository.GetOneAsync(model.UserId);
        if (user is null)
        {
            throw new InvalidOperationException("Activity cannot be without user");
        }

        ActivityEntity entity = ActivityModelMapper.MapToActivityEntity(model, user);

        if (await activityRepository.ExistsAsync(entity))
        {
            ActivityEntity updatedEntity = await activityRepository.UpdateAsync(entity);
            result = ModelMapper.MapToDetailModel(updatedEntity);
        }
        else
        {
            entity.Id = Guid.NewGuid();
            ActivityEntity insertedEntity = await activityRepository.InsertAsync(entity);
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
            .Get().Where(i => i.Start >= start && i.End <= end)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisYear()
    {
        int year = DateTime.Today.Year;
        DateTime startOfYear = new(year, 1, 1);
        DateTime endOfYear = new(year + 1, 1, 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= startOfYear && i.Start < endOfYear)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterLastMonth()
    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        DateTime lastMonthStart = new(year, month - 1, 1);
        if (month == 1)
        {
            lastMonthStart = new DateTime(year - 1, 12, 1);
        }

        DateTime lastMonthEnd = new(year, month, 1);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= lastMonthStart && i.Start < lastMonthEnd)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisMonth()
    {
        int year = DateTime.Today.Year;
        int month = DateTime.Today.Month;
        DateTime thisMonthStart = new(year, month, 1);
        DateTime thisMonthEnd = new(year, month + 1, 1);
        if (month == 12)
        {
            thisMonthEnd = new DateTime(year + 1, 1, 1);
        }

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= thisMonthStart && i.Start < thisMonthEnd)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public async Task<IEnumerable<ActivityListModel>> FilterThisWeek()
    {
        DateTime today = DateTime.Today;
        int day = (int)today.DayOfWeek;
        if (day == 0)
        {
            day = 7;
        }

        DateTime thisWeekStart = today.AddDays(1 - day);
        DateTime thisWeekEnd = thisWeekStart.AddDays(7);

        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        List<ActivityEntity> entities = await uow
            .GetRepository<ActivityEntity, ActivityEntityMapper>()
            .Get().Where(i => i.Start >= thisWeekStart && i.Start < thisWeekEnd)
            .ToListAsync();

        return ModelMapper.MapToListModel(entities);
    }

    public void GuardTwoActivitiesNotAtTheSameTime(ActivityDetailModel model)
    {
        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> repository = uow.GetRepository<ActivityEntity, ActivityEntityMapper>();
        IQueryable<ActivityEntity> activities = repository.Get();

        foreach (ActivityEntity activity in activities)
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
