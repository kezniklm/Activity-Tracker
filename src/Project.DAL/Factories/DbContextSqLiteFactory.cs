using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ProjectDbContext>
{
    private readonly DbContextOptionsBuilder<ProjectDbContext> _contextOptionsBuilder = new();
    private readonly bool _seedDemoData;

    public DbContextSqLiteFactory(string databaseName, bool seedDemoData = false)
    {
        _seedDemoData = seedDemoData;
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public ProjectDbContext CreateDbContext() => new(_contextOptionsBuilder.Options, _seedDemoData);
}
