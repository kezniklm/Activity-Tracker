namespace Project.DAL.Tests.DbContextTests;

public class DbContextUserTests : DbContextTestsBase
{
    public DbContextUserTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public async Task CreateNewUserById_Does_Not_Throw()
    {
        // Setup
        UserEntity userEntity = new()
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        // Exercise
        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualUser = await dbx.Users.SingleAsync(i => i.Id == userEntity.Id);
        DeepAssert.Equal(userEntity, actualUser);
    }

    [Fact]
    public async Task CreateNewUserByPhotoUrl_Does_Not_Throw()
    {
        // Setup
        UserEntity userEntity = new()
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        // Exercise
        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        await using ProjectDbContext dbx = await DbContextFactory.CreateDbContextAsync();
        UserEntity actualUser = await dbx.Users.SingleAsync(i => i.PhotoUrl == userEntity.PhotoUrl);
        DeepAssert.Equal(userEntity, actualUser);
    }


    [Fact]
    public async Task DeleteUserById_Does_Not_Throw()
    {
        // Setup
        UserEntity userEntity = new()
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };

        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Exercise
        ProjectDbContextSUT.Remove(ProjectDbContextSUT.Users.Single(i => i.Id == userEntity.Id));
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        Assert.False(await ProjectDbContextSUT.Users.AnyAsync(i => i.Id == userEntity.Id));
    }

    [Fact]
    public async Task DeleteUserByName_Does_Not_Throw()
    {
        // Setup
        UserEntity userEntity = new()
        {
            Id = new Guid(),
            Name = "Anton",
            Surname = "Bernolák",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/b/b5/Bernolak_Anton.jpg"
        };
        ProjectDbContextSUT.Users.Add(userEntity);
        await ProjectDbContextSUT.SaveChangesAsync();

        // Exercise
        ProjectDbContextSUT.Remove(ProjectDbContextSUT.Users.Single(i => i.Id == userEntity.Id));
        await ProjectDbContextSUT.SaveChangesAsync();

        // Verify
        Assert.False(await ProjectDbContextSUT.Users.AnyAsync(i => i.Name == userEntity.Name));
    }
}
