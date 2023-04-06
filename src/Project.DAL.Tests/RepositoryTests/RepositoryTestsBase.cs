namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryTestsBase : IAsyncLifetime
{
    protected ProjectDbContext DbContext;
    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }
    protected Repository<UserEntity> RepositorySUT { get; set; }
    protected Repository<ProjectEntity> RepositorySPT { get; set; }

    public RepositoryTestsBase()
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        DbContext = DbContextFactory.CreateDbContext();
        RepositorySUT = new Repository<UserEntity>(DbContext, new UserEntityMapper());
        RepositorySPT = new Repository<ProjectEntity>(DbContext, new ProjectEntityMapper());
    }

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
