namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryUserTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetOneUser_Does_Not_Throw()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserEntity> repositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        UserEntity? actualEntity = await repositoryUserSUT.GetOneAsync(userEntity.Id);

        // Verify
        DeepAssert.Equal(userEntity, actualEntity);
    }

    [Fact]
    public async Task UpdateUser_Does_Not_Throw()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "Anton", Surname = "Bernolák", PhotoUrl = null };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();
        UserEntity updateUserEntity = new()
        {
            Id = userEntity.Id,
            Name = userEntity.Name,
            Surname = userEntity.Surname,
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        // Exercise
        await RepositoryUserSUT.UpdateAsync(updateUserEntity);

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualEntity = await dbx.Users.SingleAsync(i => i.Id == userEntity.Id);
        Assert.NotEqual(userEntity, actualEntity);
        Assert.Equal(updateUserEntity.Name, actualEntity.Name);
        Assert.NotEqual(updateUserEntity.PhotoUrl, actualEntity.PhotoUrl);
    }

    [Fact]
    public async Task RemoveUser_Does_Not_Throw()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        RepositoryUserSUT.Delete(userEntity.Id);
        await unitOfWork.CommitAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbx.Users.AnyAsync(i => i.Name == userEntity.Name));
    }

    [Fact]
    public async Task AddUser_Does_Not_Throw()
    {
        // Setup
        IUnitOfWork unitOfWork = UnitOfWorkFactory.Create();
        IRepository<UserEntity> RepositoryUserSUT = unitOfWork.GetRepository<UserEntity, UserEntityMapper>();
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };

        // Exercise
        await RepositoryUserSUT.InsertAsync(userEntity);
        await unitOfWork.CommitAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity? actualEntity = await dbx.Users.SingleOrDefaultAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }
}
