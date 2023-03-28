namespace Project.DAL.Tests.DbContextTests;

public class DbContextUserProjectTests : DbContextTestsBase
{
    public DbContextUserProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact (Skip = "Testing pipeline")]
    public async Task Create_UserProject()
    {
        // Setup
        var userEntity = new UserEntity() { Name = "John", Surname = "Doe", Id = Guid.NewGuid()};
        var projectEntity = new ProjectEntity() { Name = "Sport", Id = Guid.NewGuid()};
        UserProjectEntity userProjectEntity = new UserProjectEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        userEntity.Projects.Add(userProjectEntity);
        projectEntity.Users.Add(userProjectEntity);

        // Exercise
        ProjectDbContextSUT.Users.Add(userEntity);
        ProjectDbContextSUT.Projects.Add(projectEntity);
        ProjectDbContextSUT.UsersProjects.Add(userProjectEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbContext.Users.SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }
}
