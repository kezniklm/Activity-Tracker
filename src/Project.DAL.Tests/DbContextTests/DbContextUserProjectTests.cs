namespace Project.DAL.Tests.DbContextTests;

public class DbContextUserProjectTests : DbContextTestsBase
{
    public DbContextUserProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task Create_UserProject_Does_Not_Throw()
    {
        // Setup
        UserEntity userEntity = new()
        {
            Name = "John", Surname = "Doe", Id = Guid.Parse("89F51C77-8362-4D55-9EA2-FD990C970EA4")
        };
        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ProjectEntity projectEntity = new() { Name = "Sport", Id = Guid.Parse("40124E0A-C1FB-456E-A32A-7188EC41A846") };
        ProjectDbContextSUT.Projects.Add(projectEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        UserProjectEntity userProjectEntity = new()
        {
            Id = Guid.Parse("07F1F95D-8AA8-4B90-A024-2AE8FB88C4CA"),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };

        // Exercise
        userEntity.Projects.Add(userProjectEntity);
        ProjectDbContextSUT.Users.Update(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualEntity = await dbContext.Users
            .Include(i => i.Projects)
            .ThenInclude(i => i.Project)
            .SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }
}
