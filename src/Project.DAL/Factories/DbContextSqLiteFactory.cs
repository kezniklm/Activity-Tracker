using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Factories;

internal class DbContextSqLiteFactory : IDbContextFactory<ProjectDbContext>
{
    private readonly DbContextOptionsBuilder<ProjectDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName)
    {
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public ProjectDbContext CreateDbContext() => new(_contextOptionsBuilder.Options);
}