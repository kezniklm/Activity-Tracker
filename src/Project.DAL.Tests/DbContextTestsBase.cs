namespace Project.DAL.Tests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected ProjectDbContext ProjectDbContextSUT { get; }
    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }

    protected DbContextTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        ProjectDbContextSUT = DbContextFactory.CreateDbContext();
    }

    public async Task InitializeAsync()
    {
        await ProjectDbContextSUT.Database.EnsureDeletedAsync();
        await ProjectDbContextSUT.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await ProjectDbContextSUT.Database.EnsureDeletedAsync();
        await ProjectDbContextSUT.DisposeAsync();
    }
}
