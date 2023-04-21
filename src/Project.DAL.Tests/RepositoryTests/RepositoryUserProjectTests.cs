namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryUserProjectTests : RepositoryTestsBase
{
    [Fact]
    public async Task AddUserProject()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        IRepository<ProjectEntity> RepositoryProjectSUT = unitOfWork.GetRepository<ProjectEntity, ProjectEntityMapper>();
        IRepository<UserProjectEntity> RepositoryUserProjectSUT = unitOfWork.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);
        await unitOfWork.CommitAsync();

        UserProjectEntity userProjectEntity = new()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };

        // Exercise
        await RepositoryUserProjectSUT.InsertAsync(userProjectEntity);
        await unitOfWork.CommitAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualEntity = await dbx.Users
            .Include(i => i.Projects)
            .ThenInclude(i => i.Project)
            .SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }

    [Fact]
    public async Task RemoveUserProject()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> RepositoryUserProjectSUT = unitOfWork.GetRepository<UserProjectEntity, UserProjectEntityMapper>();

        // Setup
        UserEntity userEntity = new UserEntity
        {
            Name = "John",
            Surname = "Doe",
            Id = Guid.Parse("89F51C77-8362-4D55-9EA2-FD990C970EA4")
        };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();

        ProjectEntity projectEntity =
            new ProjectEntity { Name = "Sport", Id = Guid.Parse("40124E0A-C1FB-456E-A32A-7188EC41A846") };
        DbContext.Projects.Add(projectEntity);
        await DbContext.SaveChangesAsync();

        UserProjectEntity userProjectEntity = new()
        {
            Id = Guid.Parse("07F1F95D-8AA8-4B90-A024-2AE8FB88C4CA"),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };

        // Exercise
        userEntity.Projects.Add(userProjectEntity);
        DbContext.Users.Update(userEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        RepositoryUserProjectSUT.Delete(userProjectEntity.Id);
        await unitOfWork.CommitAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity? user = await dbx.Users.Include(i => i.Projects).FirstOrDefaultAsync(i => i.Id == userEntity.Id);
        Assert.NotNull(user);
        Assert.Null(user.Projects.SingleOrDefault(i => i.ProjectId == projectEntity.Id));
    }

    [Fact]
    public async Task GetOneUserProject()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> RepositoryUserProjectSUT = unitOfWork.GetRepository<UserProjectEntity, UserProjectEntityMapper>();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        IRepository<ProjectEntity> RepositoryProjectSUT = unitOfWork.GetRepository<ProjectEntity, ProjectEntityMapper>();

        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);
        await unitOfWork.CommitAsync();

        UserProjectEntity userProjectEntity = new()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        await RepositoryUserProjectSUT.InsertAsync(userProjectEntity);
        await unitOfWork.CommitAsync();

        // Exercise
        UserProjectEntity? actualEntity = await RepositoryUserProjectSUT.GetOneAsync(userProjectEntity.Id);

        // Verify
        Assert.Equal(userProjectEntity, actualEntity);
    }

    [Fact]
    public async Task UpdateUserProject()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserProjectEntity> RepositoryUserProjectSUT = unitOfWork.GetRepository<UserProjectEntity, UserProjectEntityMapper>();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        IRepository<ProjectEntity> RepositoryProjectSUT = unitOfWork.GetRepository<ProjectEntity, ProjectEntityMapper>();

        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        await RepositoryUserSUT.InsertAsync(userEntity);

        ProjectEntity projectEntity = new() { Id = Guid.NewGuid(), Name = "Projekt1" };
        await RepositoryProjectSUT.InsertAsync(projectEntity);
        await unitOfWork.CommitAsync();

        UserProjectEntity userProjectEntity = new()
        {
            Id = Guid.NewGuid(),
            UserId = userEntity.Id,
            User = userEntity,
            ProjectId = projectEntity.Id,
            Project = projectEntity
        };
        await RepositoryUserProjectSUT.InsertAsync(userProjectEntity);
        await unitOfWork.CommitAsync();

        UserProjectEntity updateUserProjectEntity = new UserProjectEntity
        {
            Id = userProjectEntity.Id,
            UserId = userProjectEntity.UserId,
            User = new UserEntity { Id = userProjectEntity.User.Id, Name = "Anton", Surname = "Bernolák" },
            ProjectId = userProjectEntity.ProjectId,
            Project = userProjectEntity.Project
        };

        // Exercise
        await RepositoryUserProjectSUT.UpdateAsync(updateUserProjectEntity);
        await unitOfWork.CommitAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserProjectEntity actualEntity = await dbx.UsersProjects.SingleAsync(i => i.Id == userProjectEntity.Id);
        Assert.NotEqual(userProjectEntity, actualEntity);
        Assert.NotEqual(userProjectEntity.User, actualEntity.User);
        Assert.Equal(updateUserProjectEntity.Id, actualEntity.Id);
        Assert.Equal(updateUserProjectEntity.User.Id, actualEntity.UserId);
    }
}
