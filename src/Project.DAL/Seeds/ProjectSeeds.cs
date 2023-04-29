using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL.Seeds;

public static class ProjectSeeds
{
    public static readonly ProjectEntity Project1 = new()
    {
        Id = Guid.Parse("569E8B7C-C61E-4261-9446-7810288FB52D"), Name = "Projekt1"
    };

    public static readonly ProjectEntity Project2 = new()
    {
        Id = Guid.Parse("5A96B175-22DC-4F9C-96E0-DA7B10C6344D"), Name = "Projekt2"
    };

    public static readonly ProjectEntity Project3 = new()
    {
        Id = Guid.Parse("2383FA0A-E2D1-49D4-B0B7-719F8877E7B0"), Name = "Projekt3"
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<ProjectEntity>().HasData(
            Project1,
            Project2,
            Project3
        );
}
