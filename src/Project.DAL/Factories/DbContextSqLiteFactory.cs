﻿using Microsoft.EntityFrameworkCore;

namespace Project.DAL.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<ProjectDbContext>
{
    private readonly string _databaseName;

    public DbContextSqLiteFactory(string databaseName)
    {
        _databaseName = databaseName;
    }

    public ProjectDbContext CreateDbContext()
    {
        DbContextOptionsBuilder<ProjectDbContext> builder = new();
        builder.UseSqlite($"Data Source={_databaseName};Cache=Shared");
        return new ProjectDbContext(builder.Options);
    }
}
