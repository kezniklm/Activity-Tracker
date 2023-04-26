namespace Project.App.Options;

public record DALOptions
{
    public SqliteOptions? Sqlite { get; init; }
}

public record SqliteOptions
{
    public string DatabaseName { get; init; } = null!;
    public bool RecreateDatabaseEachTime { get; init; } = false;
    public bool SeedDemoData { get; init; } = false;
}
