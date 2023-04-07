using Project.BL.Models;

namespace Project.BL.Tests;

public class FacadeTestsBase : IAsyncLifetime
{
    protected FacadeTestsBase()
    {
        DbContextFactory = new DbContextSqLiteFactory(GetType().FullName!);

        ActivityEntityMapper = new ActivityEntityMapper();
        ProjectEntityMapper = new ProjectEntityMapper();
        UserEntityMapper = new UserEntityMapper();
        UserProjectEntityMapper = new UserProjectEntityMapper();

        ActivityModelMapper = new ActivityModelMapper();
        ProjectModelMapper = new ProjectModelMapper();
        UserModelMapper = new UserModelMapper();
        UserProjectModelMapper = new UserProjectModelMapper();

        UnitOfWorkFactory = new UnitOfWorkFactory(DbContextFactory);
    }

    protected IDbContextFactory<ProjectDbContext> DbContextFactory { get; }

    protected ActivityEntityMapper ActivityEntityMapper { get; }
    protected ProjectEntityMapper ProjectEntityMapper { get; }
    protected UserEntityMapper UserEntityMapper { get; }
    protected UserProjectEntityMapper UserProjectEntityMapper { get; }

    protected ActivityModelMapper ActivityModelMapper { get; }
    protected ProjectModelMapper ProjectModelMapper { get; }
    protected UserModelMapper UserModelMapper { get; }
    protected UserProjectModelMapper UserProjectModelMapper { get; }

    protected UnitOfWorkFactory UnitOfWorkFactory { get; }

    public async Task InitializeAsync()
    {
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }

    public async Task DisposeAsync()
    {
        await using var dbContext = await DbContextFactory.CreateDbContextAsync();
        await dbContext.Database.EnsureDeletedAsync();
    }

    public static void FixIds(ModelBase expectedDetail, ModelBase actualDetail)
    {
        actualDetail.Id = expectedDetail.Id;
    }
}