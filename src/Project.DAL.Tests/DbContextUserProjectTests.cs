namespace Project.DAL.Tests;

public class DbContextUserProjectTests : DbContextTestsBase
{
    public DbContextUserProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Create_UserProject()
    {
        // Setup
        var userEntity = new UserEntity() { Name = "John", Surname = "Doe" };
        var projectEntity = new ProjectEntity() { Name = "Sport" };
        userEntity.Projects.Add(new UserProjectEntity() { User = userEntity, Project = projectEntity, });

        // Exercise
        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbContext.Users.SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }
}
