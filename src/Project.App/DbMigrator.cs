using Microsoft.EntityFrameworkCore;
using Project.App.Options;
using Project.DAL;

namespace Project.App;

public interface IDbMigrator
{
    public void Migrate();
    public Task MigrateAsync(CancellationToken cancellationToken);
}

public class DbMigrator : IDbMigrator
{
    private readonly IDbContextFactory<ProjectDbContext> _dbContextFactory;
    private readonly SqliteOptions _options;

    public DbMigrator(IDbContextFactory<ProjectDbContext> dbContextFactory, DALOptions dalOptions)
    {
        _dbContextFactory = dbContextFactory;
        _options = dalOptions.Sqlite ??
                   throw new ArgumentNullException(nameof(dalOptions), $@"{nameof(DALOptions.Sqlite)} are not set");
    }

    public void Migrate() => MigrateAsync(CancellationToken.None).GetAwaiter().GetResult();

    public async Task MigrateAsync(CancellationToken cancellationToken)
    {
        await using ProjectDbContext dbContext = await _dbContextFactory.CreateDbContextAsync(cancellationToken);

        if (_options.RecreateDatabaseEachTime)
        {
            await dbContext.Database.EnsureDeletedAsync(cancellationToken);
        }

        await dbContext.Database.EnsureCreatedAsync(cancellationToken);
    }
}
