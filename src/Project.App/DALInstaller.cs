using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Project.App.Options;
using Project.DAL;
using Project.DAL.Factories;
using Project.DAL.Mappers;

namespace Project.App;

public static class DALInstaller
{
    public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
    {
        DALOptions dalOptions = new();
        configuration.GetSection("Project:DAL").Bind(dalOptions);

        services.AddSingleton(dalOptions);

        if (dalOptions.Sqlite is null)
        {
            throw new InvalidOperationException("No persistence provider configured");
        }

        if (dalOptions.Sqlite.DatabaseName is null)
        {
            throw new InvalidOperationException($"{nameof(dalOptions.Sqlite.DatabaseName)} is not set");
        }

        string databaseFilePath = Path.Combine(FileSystem.AppDataDirectory, dalOptions.Sqlite.DatabaseName);
        services.AddSingleton<IDbContextFactory<ProjectDbContext>>(provider =>
            new DbContextSqLiteFactory(databaseFilePath, dalOptions.Sqlite?.SeedDemoData ?? false));
        services.AddSingleton<IDbMigrator, DbMigrator>();

        services.AddSingleton<UserEntityMapper>();
        services.AddSingleton<ActivityEntityMapper>();
        services.AddSingleton<ProjectEntityMapper>();
        services.AddSingleton<UserProjectEntityMapper>();

        return services;
    }
}
