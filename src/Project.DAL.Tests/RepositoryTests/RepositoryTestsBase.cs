namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryTestsBase : IAsyncLifetime
{
    protected ProjectDbContext DbContext;

    public RepositoryTestsBase()
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        DbContext = DbContextFactory.CreateDbContext();
        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }
    protected IUnitOfWorkFactory UnitOfWorkFactory { get; set; }

    public async Task InitializeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await DbContext.Database.EnsureDeletedAsync();
        await DbContext.DisposeAsync();
    }
}
