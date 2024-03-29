namespace Project.DAL.Tests.DbContextTests;

public class DbContextTestsBase : IAsyncLifetime
{
    protected DbContextTestsBase(ITestOutputHelper output)
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        ProjectDbContextSUT = DbContextFactory.CreateDbContext();
    }

    protected ProjectDbContext ProjectDbContextSUT { get; }
    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }

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
