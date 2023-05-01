using Microsoft.EntityFrameworkCore;
using Project.DAL.Entities;

namespace Project.DAL.Seeds;

public static class UserProjectsSeeds
{
    public static readonly UserProjectEntity UserProjectEntity1 = new()
    {
        Id = Guid.Parse("9B62038F-7B8A-481C-ABD4-03E273F6FDA7"),
        ProjectId = ProjectSeeds.Project1.Id,
        UserId = UserSeeds.User1.Id
    };

    public static readonly UserProjectEntity UserProjectEntity2 = new()
    {
        Id = Guid.Parse("9B62038F-7B8A-481C-ABD4-03E273F6FDA8"),
        ProjectId = ProjectSeeds.Project2.Id,
        UserId = UserSeeds.User1.Id
    };

    public static readonly UserProjectEntity UserProjectEntity3 = new()
    {
        Id = Guid.Parse("9B62038F-7B8A-481C-ABD4-03E273F6FDA9"),
        ProjectId = ProjectSeeds.Project3.Id,
        UserId = UserSeeds.User2.Id
    };

    public static void Seed(this ModelBuilder modelBuilder) =>
        modelBuilder.Entity<UserProjectEntity>().HasData(
            UserProjectEntity1,
            UserProjectEntity2,
            UserProjectEntity3
        );
}
