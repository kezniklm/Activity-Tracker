using Project.BL.Facades.Interfaces;
using Project.BL.Facades;
using Project.BL.Models;
using Project.Common.Tests;
using Project.DAL.Entities;

namespace Project.BL.Tests;

public class FacadeActivityTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;

    public FacadeActivityTests()
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact]
    public async Task Create_New_Activity()
    {
        // Setup
        ActivityDetailModel activityDetail = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        // Exercise
        ActivityDetailModel actualDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Verify
        FixIds(activityDetail, actualDetail);
        DeepAssert.Equal(activityDetail, actualDetail);
    }

    [Fact]
    public async Task Create_New_Activity_SameTime()
    {
        // Setup
        ActivityDetailModel activityDetail1 = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        await _activityFacadeSUT.SaveAsync(activityDetail1);

        ActivityDetailModel activityDetail2 = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 14, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        // Exercise & Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacadeSUT.SaveAsync(activityDetail2));
    }

    [Fact]
    public async Task Create_New_Activity_StartIsGreater()
    {
        // Setup
        ActivityDetailModel activityDetail = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 16, 0, 0),
            End = new DateTime(2023, 3, 20, 15, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        // Exercise & Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacadeSUT.SaveAsync(activityDetail));
    }

    [Fact]
    public async Task Get_OneActivity()
    {
        // Setup
        ActivityDetailModel activityDetail = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };
        ActivityDetailModel expectedDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Exercise
        ActivityDetailModel? actualDetail = await _activityFacadeSUT.GetAsync(expectedDetail.Id);

        // Verify
        DeepAssert.Equal(expectedDetail, actualDetail);
    }

    [Fact]
    public async Task Get_All_Activities()
    {
        // Setup
        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };
        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity);
        await dbContext.SaveChangesAsync();
        ActivityListModel activityList = ActivityModelMapper.MapToListModel(activityEntity);

        // Exercise
        IEnumerable<ActivityListModel> actualList = await _activityFacadeSUT.GetAsync();

        // Verify
        Assert.Contains(activityList, actualList);
    }

    [Fact]
    public async Task Delete_Activity()
    {
        // Setup
        ActivityDetailModel activityDetail = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };
        ActivityDetailModel expectedDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Exercise
        await _activityFacadeSUT.DeleteAsync(expectedDetail.Id);

        // Verify
        ActivityDetailModel? actualDetail = await _activityFacadeSUT.GetAsync(expectedDetail.Id);
        Assert.Null(actualDetail);
    }

    [Fact]
    public async Task Activity_Filter_Start_End()
    {
        // Setup
        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 22, 15, 0, 0),
            End = new DateTime(2023, 3, 22, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity3 = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 23, 15, 0, 0),
            End = new DateTime(2023, 3, 23, 16, 0, 0),
            User = new UserEntity() { Name = "Name", Surname = "Surname" }
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity1);
        dbContext.Activities.Add(activityEntity2);
        dbContext.Activities.Add(activityEntity3);
        await dbContext.SaveChangesAsync();

        ActivityListModel list1 = ActivityModelMapper.MapToListModel(activityEntity1);
        ActivityListModel list2 = ActivityModelMapper.MapToListModel(activityEntity2);
        ActivityListModel list3 = ActivityModelMapper.MapToListModel(activityEntity3);

        DateTime start = new(2023, 3, 21, 15, 0, 0);
        DateTime end = new(2023, 3, 24, 15, 0, 0);

        // Exercise
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.Filter(start, end);

        // Verify
        IEnumerable<ActivityListModel> activityListModels = filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.DoesNotContain(list1, activityListModels);
        Assert.Contains(list2, activityListModels);
        Assert.Contains(list3, activityListModels);
    }

    private static void FixIds(ModelBase expectedDetail, ModelBase actualDetail)
    {
        actualDetail.Id = expectedDetail.Id;
    }
}
