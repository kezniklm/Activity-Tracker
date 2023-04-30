using System;

namespace Project.BL.Tests;

public class FacadeActivityTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;
    private readonly IUserFacade _userFacadeSUT;

    public FacadeActivityTests()
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_New_Activity_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };

        // Exercise
        ActivityDetailModel actualDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Verify
        FixIds(activityDetail, actualDetail);
        DeepAssert.Equal(activityDetail, actualDetail);
    }

    [Fact]
    public async Task Create_New_Activity_SameTime_Throws()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail1 = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };

        await _activityFacadeSUT.SaveAsync(activityDetail1);

        ActivityDetailModel activityDetail2 = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 14, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };

        // Exercise & Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacadeSUT.SaveAsync(activityDetail2));
    }

    [Fact]
    public async Task Create_New_Activity_StartIsGreater_Throws()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 16, 0, 0),
            End = new DateTime(2023, 3, 20, 15, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };

        // Exercise & Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _activityFacadeSUT.SaveAsync(activityDetail));
    }

    [Fact]
    public async Task Get_OneActivity_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };
        ActivityDetailModel expectedDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Exercise
        ActivityDetailModel? actualDetail = await _activityFacadeSUT.GetAsync(expectedDetail.Id, "User");

        // Verify
        DeepAssert.Equal(expectedDetail, actualDetail);
    }

    [Fact]
    public async Task Get_All_Activities_Does_Not_Throw()
    {
        // Setup
        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity { Id = Guid.NewGuid(), Name = "Name", Surname = "Surname" }
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
    public async Task Delete_Activity_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };
        ActivityDetailModel expectedDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        // Exercise
        await _activityFacadeSUT.DeleteAsync(expectedDetail.Id);

        // Verify
        ActivityDetailModel? actualDetail = await _activityFacadeSUT.GetAsync(expectedDetail.Id, "User");
        Assert.Null(actualDetail);
    }

    [Fact]
    public async Task Activity_Filter_Start_End_Does_Not_Throw()
    {
        // Setup
        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72C8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E1C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72C8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 22, 15, 0, 0),
            End = new DateTime(2023, 3, 22, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E0C72C8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity3 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 23, 15, 0, 0),
            End = new DateTime(2023, 3, 23, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
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
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.Filter(start, end, activityEntity3.UserId);

        // Verify
        IEnumerable<ActivityListModel> activityListModels =
            filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.DoesNotContain(list1, activityListModels);
        Assert.DoesNotContain(list2, activityListModels);
        Assert.Contains(list3, activityListModels);
    }

    [Fact]
    public async Task Test_FilterThisYear_Does_Not_Throw()
    {
        //Setup
        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2022, 3, 22, 15, 0, 0),
            End = new DateTime(2022, 3, 22, 16, 0, 0),
            User = activityEntity1.User
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity1);
        dbContext.Activities.Add(activityEntity2);
        await dbContext.SaveChangesAsync();

        ActivityListModel list1 = ActivityModelMapper.MapToListModel(activityEntity1);
        ActivityListModel list2 = ActivityModelMapper.MapToListModel(activityEntity2);

        // Exercise
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.FilterThisYear(activityEntity1.UserId);

        // Verify
        IEnumerable<ActivityListModel> activityListModels =
            filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.Contains(list1, activityListModels);
        Assert.DoesNotContain(list2, activityListModels);
    }

    [Fact]
    public async Task Test_FilterThisMonth_Does_Not_Throw()
    {
        //Setup
        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 1, 20, 15, 0, 0),
            End = new DateTime(2023, 1, 20, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 4, 5, 15, 0, 0),
            End = new DateTime(2023, 4, 5, 16, 0, 0),
            User = activityEntity1.User
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity1);
        dbContext.Activities.Add(activityEntity2);
        await dbContext.SaveChangesAsync();

        ActivityListModel list1 = ActivityModelMapper.MapToListModel(activityEntity1);
        ActivityListModel list2 = ActivityModelMapper.MapToListModel(activityEntity2);

        // Exercise
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.FilterThisMonth(activityEntity1.UserId);

        // Verify
        IEnumerable<ActivityListModel> activityListModels =
            filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.DoesNotContain(list1, activityListModels);
        Assert.Contains(list2, activityListModels);
    }

    [Fact]
    public async Task Test_FilterLastMonth_Does_Not_Throw()
    {
        //Setup
        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 4, 1, 15, 0, 0),
            End = new DateTime(2023, 4, 1, 16, 0, 0),
            User = new UserEntity { Id = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 31, 15, 0, 0),
            End = new DateTime(2023, 3, 31, 16, 0, 0),
            User = activityEntity1.User
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity1);
        dbContext.Activities.Add(activityEntity2);
        await dbContext.SaveChangesAsync();

        ActivityListModel list1 = ActivityModelMapper.MapToListModel(activityEntity1);
        ActivityListModel list2 = ActivityModelMapper.MapToListModel(activityEntity2);

        // Exercise
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.FilterLastMonth(activityEntity1.UserId);

        // Verify
        IEnumerable<ActivityListModel> activityListModels =
            filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.DoesNotContain(list1, activityListModels);
        Assert.Contains(list2, activityListModels);
    }

    [Fact]
    public async Task Test_FilterThisWeek_Does_Not_Throw()
    {
        //Setup

        DateTime today = DateTime.Today;

        ActivityEntity activityEntity1 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = today.AddDays(-8),
            End = today.AddDays(-8),
            User = new UserEntity { Id = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"), Name = "Name", Surname = "Surname" }
        };

        ActivityEntity activityEntity2 = new()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.Parse("0E0C72D8-C205-4C07-B515-59EB01226056"),
            ActivityType = "Activity",
            Start = today,
            End = today,
            User = activityEntity1.User
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Activities.Add(activityEntity1);
        dbContext.Activities.Add(activityEntity2);
        await dbContext.SaveChangesAsync();

        ActivityListModel list1 = ActivityModelMapper.MapToListModel(activityEntity1);
        ActivityListModel list2 = ActivityModelMapper.MapToListModel(activityEntity2);

        // Exercise
        IEnumerable<ActivityListModel> filteredList = await _activityFacadeSUT.FilterThisWeek(activityEntity1.UserId);

        // Verify
        IEnumerable<ActivityListModel> activityListModels =
            filteredList as ActivityListModel[] ?? filteredList.ToArray();
        Assert.DoesNotContain(list1, activityListModels);
        Assert.Contains(list2, activityListModels);
    }

    [Fact]
    public async Task UpdateActivity_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activity = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };
        ActivityDetailModel newActivity = await _activityFacadeSUT.SaveAsync(activity);

        // Exercise
        newActivity.ActivityType = "Run";
        ActivityDetailModel updatedActivity = await _activityFacadeSUT.SaveAsync(newActivity);

        // Verify
        await using ProjectDbContext dbxAssert = await DbContextFactory.CreateDbContextAsync();
        ActivityEntity activityFromDb =
            await dbxAssert.Activities.Include(i => i.User).SingleAsync(i => i.Id == updatedActivity.Id);
        DeepAssert.Equal(updatedActivity, ActivityModelMapper.MapToDetailModel(activityFromDb));
    }
}
