using Project.DAL.Entities;

namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryUserProjectTests : RepositoryTestsBase
{

    [Fact]
    public async Task AddUserProject()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);

        var userProjectEntity = new UserProjectEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };

        // Exercise
        userEntity.Projects.Add(userProjectEntity);
        await DbContext.SaveChangesAsync();

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users
            .Include(i => i.Projects)
            .ThenInclude(i => i.Project)
            .SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }

    [Fact]
    public async Task RemoveUserProject()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);

        var userProjectEntity = new UserProjectEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        userEntity.Projects.Add(userProjectEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        RepositoryUserProjectSUT.Delete(userProjectEntity.Id);

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var user = await dbx.Users.Include(i => i.Projects).FirstOrDefaultAsync(i => i.Id == userEntity.Id);
        Assert.NotNull(user);
        Assert.False(user.Projects.Any(i => i.ProjectId == projectEntity.Id));
    }

    [Fact]
    public async Task GetOneUserProject()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);

        var userProjectEntity = new UserProjectEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        await RepositoryUserProjectSUT.InsertAsync(userProjectEntity);

        // Exercise
        UserProjectEntity actualEntity = RepositoryUserProjectSUT.GetOne(userProjectEntity.Id);

        // Verify
        Assert.Equal(userProjectEntity, actualEntity);
    }

    [Fact]
    public async Task UpdateUserProject()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);

        var userProjectEntity = new UserProjectEntity()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        await RepositoryUserProjectSUT.InsertAsync(userProjectEntity);

        var updateUserProjectEntity = new UserProjectEntity()
        {
            Id = userProjectEntity.Id,
            UserId = userProjectEntity.UserId,
            User = new() { Id = userProjectEntity.User.Id, Name = "Anton", Surname = "Bernolák" },
            ProjectId = userProjectEntity.ProjectId,
            Project = userProjectEntity.Project
        };

        // Exercise
        await RepositoryUserProjectSUT.UpdateAsync(updateUserProjectEntity);

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.UsersProjects.SingleAsync(i => i.Id == userProjectEntity.Id);
        Assert.NotEqual(userProjectEntity, actualEntity);
        Assert.NotEqual(userProjectEntity.User, actualEntity.User);
        Assert.Equal(updateUserProjectEntity.Id, actualEntity.Id);
        Assert.Equal(updateUserProjectEntity.User.Id, actualEntity.UserId);
    }
} 

