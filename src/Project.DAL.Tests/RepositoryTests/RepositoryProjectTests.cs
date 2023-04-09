namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryProjectTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetOneProject()
    {
        // Setup
        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        DbContext.Projects.Add(projectEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        ProjectEntity actualEntity = RepositoryProjectSUT.GetOne(projectEntity.Id);

        // Verify
        Assert.Equal(projectEntity, actualEntity);
    }

    [Fact]
    public async Task UpdateProject()
    {
        // Setup
        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt2" };
        DbContext.Projects.Add(projectEntity);
        await DbContext.SaveChangesAsync();
        ProjectEntity updateProjectEntity = new() { Id = projectEntity.Id, Name = projectEntity.Name };

        // Exercise
        await RepositoryProjectSUT.UpdateAsync(updateProjectEntity);

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ProjectEntity actualEntity = await dbx.Projects.SingleAsync(i => i.Id == projectEntity.Id);
        Assert.NotEqual(projectEntity, actualEntity);
        Assert.Equal(updateProjectEntity.Name, actualEntity.Name);
    }

    [Fact]
    public async Task RemoveProject()
    {
        // Setup
        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        DbContext.Projects.Add(projectEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        RepositoryProjectSUT.Delete(projectEntity.Id);

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbx.Projects.AnyAsync(i => i.Name == projectEntity.Name));
    }

    [Fact]
    public async Task AddProject()
    {
        // Setup
        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };

        // Exercise
        await RepositoryProjectSUT.InsertAsync(projectEntity);

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        ProjectEntity? actualEntity = await dbx.Projects.SingleOrDefaultAsync(i => i.Id == projectEntity.Id);
        DeepAssert.Equal(projectEntity, actualEntity);
    }
}
