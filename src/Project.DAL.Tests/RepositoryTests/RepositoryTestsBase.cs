namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryTestsBase : IAsyncLifetime
{
    protected ProjectDbContext DbContext;
    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }
    protected Repository<UserEntity> RepositoryUserSUT { get; set; }
    protected Repository<ProjectEntity> RepositoryProjectSUT { get; set; }
    protected Repository<ActivityEntity> RepositoryActivitySUT { get; set; }
    protected Repository<UserProjectEntity> RepositoryUserProjectSUT { get; set; }

    public RepositoryTestsBase()
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);
        DbContext = DbContextFactory.CreateDbContext();
        RepositoryUserSUT = new Repository<UserEntity>(DbContext, new UserEntityMapper());
        RepositoryProjectSUT = new Repository<ProjectEntity>(DbContext, new ProjectEntityMapper());
        RepositoryActivitySUT = new Repository<ActivityEntity>(DbContext, new ActivityEntityMapper());
        RepositoryUserProjectSUT = new Repository<UserProjectEntity>(DbContext, new UserProjectEntityMapper());
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
