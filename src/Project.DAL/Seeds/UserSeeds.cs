using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL.Seeds;

public static class UserSeeds
{
    public static readonly UserEntity User1 = new()
    {
        Id = Guid.Parse("269E8B7C-C61E-4261-9446-7810288FB52D"), Name = "John", Surname = "Doe"
    };

    public static readonly UserEntity User2 = new()
    {
        Id = Guid.Parse("8A96B175-22DC-4F9C-96E0-DA7B10C6344D"), Name = "Oliver", Surname = "Smith"
    };

    public static readonly UserEntity User3 = new()
    {
        Id = Guid.Parse("5383FA0A-E2D1-49D4-B0B7-719F8877E7B0"), Name = "Sophia", Surname = "Green"
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserEntity>().HasData(
            User1,
            User2,
            User3
        );
}
