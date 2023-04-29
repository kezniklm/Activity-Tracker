using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL.Seeds;

public static class ActivitySeeds
{
    public static readonly ActivityEntity Activity1 = new()
    {
        Id = Guid.Parse("269E8B7C-C61E-4261-9446-6810288FB52D"),
        ActivityType = "Projekt",
        Start = new DateTime(2023, 03, 05, 10, 0, 0),
        End = new DateTime(2023, 03, 05, 11, 0, 0),
        UserId = Guid.Parse("269E8B7C-C61E-4261-9446-7810288FB52D"),
    };

    public static readonly ActivityEntity Activity2 = new()
    {
        Id = Guid.Parse("8A96B175-22DC-4F9C-96E0-6A7B10C6344D"),
        ActivityType = "beh",
        Start = new DateTime(2023, 03, 05, 12, 0, 0),
        End = new DateTime(2023, 03, 05, 13, 0, 0),
        UserId = Guid.Parse("269E8B7C-C61E-4261-9446-7810288FB52D"),
    };

    public static readonly ActivityEntity Activity3 = new()
    {
        Id = Guid.Parse("5383FA0A-E2D1-49D4-B0B7-619F8877E7B0"),
        ActivityType = "skola",
        Start = new DateTime(2023, 03, 05, 14, 0, 0),
        End = new DateTime(2023, 03, 05, 15, 0, 0),
        UserId = Guid.Parse("269E8B7C-C61E-4261-9446-7810288FB52D"),
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ActivityEntity>().HasData(
            Activity1,
            Activity2,
            Activity3
        );
}
