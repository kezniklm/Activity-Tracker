using Microsoft.EntityFrameworkCore.Design;

namespace Project.DAL.Factories;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ProjectDbContext>
{
    private readonly DbContextSqLiteFactory _dbContextSqLiteFactory;
    private const string ConnectionString = $"Data Source=Project;Cache=Shared";

    public DesignTimeDbContextFactory()
    {
        _dbContextSqLiteFactory = new DbContextSqLiteFactory(ConnectionString);
    }

    public ProjectDbContext CreateDbContext(string[] args) => _dbContextSqLiteFactory.CreateDbContext();
}
