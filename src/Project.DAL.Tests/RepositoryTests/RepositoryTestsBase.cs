namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryTestsBase : IAsyncLifetime
{
    protected ProjectDbContext DbContext;
    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }
    protected Repository<UserEntity> RepositoryUserSUT { get; set; }
    protected Repository<ProjectEntity> RepositoryProjectSUT { get; set; }

    public RepositoryTestsBase()
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        DbContext = DbContextFactory.CreateDbContext();
        RepositoryUserSUT = new Repository<UserEntity>(DbContext, new UserEntityMapper());
        RepositoryProjectSUT = new Repository<ProjectEntity>(DbContext, new ProjectEntityMapper());
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
