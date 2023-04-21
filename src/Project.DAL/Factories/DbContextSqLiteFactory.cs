using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ProjectDbContext>
{
    private readonly bool _seedDemoData;
    private readonly DbContextOptionsBuilder<ProjectDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName, bool seedDemoData = false)
    {
        _seedDemoData = seedDemoData;
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public ProjectDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedDemoData);
}
