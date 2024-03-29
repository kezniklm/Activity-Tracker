﻿namespace Project.DAL.Tests.DbContextTests;

public class DbContextActivityTests : DbContextTestsBase
{
    public DbContextActivityTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Add_NewActivity_Does_Not_Throw()
    {
        //Arrange
        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Name", Surname = "Surname" };

        ActivityEntity entity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = "Football",
            Start = new DateTime(2023, 03, 05, 10, 0, 0),
            End = new DateTime(2023, 03, 05, 12, 0, 0),
            User = user,
            UserId = user.Id
        };

        //Act
        ProjectDbContextSUT.Activities.Add(entity);
        await ProjectDbContextSUT.SaveChangesAsync();

        //Assert
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ActivityEntity actualEntity = await dbx.Activities
            .Include(i => i.User)
            .SingleAsync(i => i.Id == entity.Id);
        DeepAssert.Equal(entity, actualEntity);
    }

    [Fact]
    public async Task GetByTime_Activity_Does_Not_Throw()
    {
        //Arrange
        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Name", Surname = "Surname" };

        ActivityEntity entity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = "Football",
            Start = new DateTime(2023, 02, 10, 15, 0, 0),
            End = new DateTime(2023, 02, 10, 16, 30, 0),
            User = user,
            UserId = user.Id
        };

        //Act
        ProjectDbContextSUT.Activities.Add(entity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ActivityEntity actualEntity = await ProjectDbContextSUT.Activities
            .SingleAsync(i => i.Start == entity.Start);

        //Assert
        Assert.Equal(entity.Start, actualEntity.Start);
    }

    [Fact]
    public async Task Delete_Activity_Does_Not_Throw()
    {
        //Arrange
        UserEntity user = new() { Id = Guid.NewGuid(), Name = "Name", Surname = "Surname" };

        ActivityEntity entity = new()
        {
            Id = Guid.NewGuid(),
            ActivityType = "Sport",
            Description = "Football",
            Start = new DateTime(2023, 02, 10, 15, 0, 0),
            End = new DateTime(2023, 02, 10, 16, 30, 0),
            User = user,
            UserId = user.Id
        };

        //Act
        ProjectDbContextSUT.Activities.Add(entity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ProjectDbContextSUT.Activities.Remove(entity);
        await ProjectDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ProjectDbContextSUT.Activities.AnyAsync(i => i.Id == entity.Id));
    }
}
