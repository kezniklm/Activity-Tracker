using Project.DAL.Entities;

namespace Project.DAL.Tests.RepositoryTests;
public class RepositoryActivityTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetOneActivity()
    {
        //Arrange
        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Harry",
            Surname = "Potter"
        };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = "Football",
            Start = new DateTime(2023, 03, 05, 10, 0, 0),
            End = new DateTime(2023, 03, 05, 12, 0, 0),
            User = user,
            UserId = user.Id,
        };

        DbContext.Activities.Add(activityEntity);
        await DbContext.SaveChangesAsync();

        //Act
        ActivityEntity actualEntity = RepositoryActivitySUT.GetOne(activityEntity.Id);

        //Assert
        Assert.Equal(activityEntity, actualEntity);
    }

    [Fact]
    public async Task AddActivity()
    {
        //Arrange
        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Ronald",
            Surname = "Weasley"
        };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "School",
            Description = "Maths",
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id,
        };

        //Act
        await RepositoryActivitySUT.InsertAsync(activityEntity);

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities
            .Include(i => i.User)
            .SingleAsync(i => i.Id == activityEntity.Id);
        DeepAssert.Equal(activityEntity, actualEntity);
    }

    [Fact]
    public async Task DeleteActivity()
    {
        //Arrange
        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Ronald",
            Surname = "Weasley"
        };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "School",
            Description = "Maths",
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id,
        };

        DbContext.Activities.Add(activityEntity);
        await DbContext.SaveChangesAsync();

        //Act
        RepositoryActivitySUT.Delete(activityEntity.Id);

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbx.Activities.AnyAsync(i => i.ActivityType == activityEntity.ActivityType));
    }

    [Fact]
    public async Task UpdateActivity()
    {
        //Arrange
        UserEntity user = new()
        {
            Id = Guid.NewGuid(),
            Name = "Harry",
            Surname = "Potter"
        };

        ActivityEntity activityEntity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = null,
            Start = new DateTime(2023, 02, 10, 18, 30, 0),
            End = new DateTime(2023, 02, 10, 19, 50, 0),
            User = user,
            UserId = user.Id,
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
            UserId = activityEntity.UserId,
        };

        //Act
        RepositoryActivitySUT.UpdateAsync(updateActivityEntity);

        //Assert
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Activities.SingleAsync(i => i.Id == activityEntity.Id);
        Assert.NotEqual(activityEntity, actualEntity);
        Assert.NotEqual(activityEntity.Description, actualEntity.Description);
        Assert.Equal(updateActivityEntity, activityEntity);
    }
}
