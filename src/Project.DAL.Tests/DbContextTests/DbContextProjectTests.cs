namespace Project.DAL.Tests.DbContextTests;

public class DbContextProjectTests : DbContextTestsBase
{
    public DbContextProjectTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateNewProjectById()
    {
        ProjectEntity projectEntity = new ProjectEntity { Id = new Guid(), Name = "IPP" };

        ProjectDbContextSUT.Projects.Add(projectEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ProjectEntity actualProject = await dbx.Projects.SingleAsync(i => i.Id == projectEntity.Id);
        DeepAssert.Equal(projectEntity, actualProject);
    }

    [Fact]
    public async Task DeleteProjectById()
    {
        ProjectEntity projectEntity = new ProjectEntity { Id = new Guid(), Name = "ICS" };

        ProjectDbContextSUT.Projects.Add(projectEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        ProjectDbContextSUT.Remove(ProjectDbContextSUT.Projects.Single(i => i.Id == projectEntity.Id));
        await ProjectDbContextSUT.SaveChangesAsync();

        //Assert
        Assert.False(await ProjectDbContextSUT.Projects.AnyAsync(i => i.Id == projectEntity.Id));
    }
}
