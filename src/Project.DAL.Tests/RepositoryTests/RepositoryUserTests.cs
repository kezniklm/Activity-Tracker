namespace Project.DAL.Tests.RepositoryTests;

public class RepositoryUserTests : RepositoryTestsBase
{
    [Fact]
    public async Task GetOneUser()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        UserEntity actualEntity = RepositorySUT.GetOne(userEntity.Id);

        // Verify
        Assert.Equal(userEntity, actualEntity);
    }

    [Fact]
    public async Task UpdateUser()
    {
        // Setup
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
        await RepositorySUT.UpdateAsync(updateUserEntity);

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleAsync(i => i.Id == userEntity.Id);
        Assert.NotEqual(userEntity, actualEntity);
        Assert.Equal(updateUserEntity.Name, actualEntity.Name);
        Assert.NotEqual(updateUserEntity.PhotoUrl, actualEntity.PhotoUrl);
    }

    [Fact]
    public async Task RemoveUser()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };
        DbContext.Users.Add(userEntity);
        await DbContext.SaveChangesAsync();

        // Exercise
        RepositorySUT.Delete(userEntity.Id);

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbx.Users.AnyAsync(i => i.Name == userEntity.Name));
    }

    [Fact]
    public async Task AddUser()
    {
        // Setup
        UserEntity userEntity = new() { Id = Guid.NewGuid(), Name = "John", Surname = "Doe" };

        // Exercise
        await RepositorySUT.InsertAsync(userEntity);

        // Verify
        await using var dbx = await DbContextFactory.CreateDbContextAsync();
        var actualEntity = await dbx.Users.SingleOrDefaultAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualEntity);
    }
}
