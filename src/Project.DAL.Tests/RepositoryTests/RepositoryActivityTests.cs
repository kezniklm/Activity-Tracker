﻿namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryActivityTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetOneActivity_Does_Not_Throw()
    {
        //Arrange
        IRepository<ActivityEntity> RepositoryActivitySUT =
            new ActivityRepository(DbContext, new ActivityEntityMapper());

        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Harry", Surname = "Potter" };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = "Football",
            Start = new DateTime(2023, 03, 05, 10, 0, 0),
            End = new DateTime(2023, 03, 05, 12, 0, 0),
            User = user,
            UserId = user.Id
        };

        DbContext.Activities.Add(activityEntity);
        await DbContext.SaveChangesAsync();

        //Act
        ActivityEntity? actualEntity = await RepositoryActivitySUT.GetOneAsync(activityEntity.Id);

        //Assert
        DeepAssert.Equal(activityEntity, actualEntity);
    }

    [Fact]
    public async Task AddActivity()
    {
        //Arrange
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> RepositoryActivitySUT =
            unitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>();

        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Ronald", Surname = "Weasley" };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "School",
            Description = "Maths",
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id
        };

        //Act
        await RepositoryActivitySUT.InsertAsync(activityEntity);
        await unitOfWork.CommitAsync();

        //Assert
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ActivityEntity actualEntity = await dbx.Activities
            .Include(i => i.User)
            .SingleAsync(i => i.Id == activityEntity.Id);
        DeepAssert.Equal(activityEntity, actualEntity);
    }

    [Fact]
    public async Task DeleteActivity()
    {
        //Arrange
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> RepositoryActivitySUT =
            unitOfWork.GetRepository<ActivityEntity, ActivityEntityMapper>();
        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Ronald", Surname = "Weasley" };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "School",
            Description = "Maths",
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id
        };

        DbContext.Activities.Add(activityEntity);
        await DbContext.SaveChangesAsync();

        //Act
        RepositoryActivitySUT.Delete(activityEntity.Id);
        await unitOfWork.CommitAsync();

        //Assert
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbx.Activities.AnyAsync(i => i.ActivityType == activityEntity.ActivityType));
    }

    [Fact]
    public async Task UpdateActivity()
    {
        //Arrange
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<ActivityEntity> RepositoryActivitySUT =
            new ActivityRepository(DbContext, new ActivityEntityMapper());

        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Harry", Surname = "Potter" };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = null,
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id
        };

        DbContext.Activities.Add(activityEntity);
        await DbContext.SaveChangesAsync();

        ActivityEntity updateActivityEntity = new()
        {
            Id = activityEntity.Id,
            ActivityType = activityEntity.ActivityType,
            Description = "Football",
            Start = activityEntity.Start,
            End = activityEntity.End,
            User = activityEntity.User,
            UserId = activityEntity.UserId
        };

        //Act
        await RepositoryActivitySUT.UpdateAsync(updateActivityEntity);
        await unitOfWork.CommitAsync();

        //Assert
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ActivityEntity actualEntity = await dbx.Activities.SingleAsync(i => i.Id == activityEntity.Id);
        Assert.NotEqual(activityEntity, actualEntity);
        Assert.NotEqual(activityEntity.Description, actualEntity.Description);
        Assert.Equal(updateActivityEntity, activityEntity);
    }
}
