namespace Project.BL.Tests;

public class FacadeUserTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;
    private readonly IActivityFacade _activityFacadeSUT;

    public FacadeUserTests()
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Setup
        UserDetailModel userDetail = new() { Name = "Anton", Surname = "Bernolak", PhotoUrl = null };

        // Exercise
        UserDetailModel expectedDetail = await _userFacadeSUT.SaveAsync(userDetail);
        FixIds(expectedDetail, userDetail);

        //Verify
        DeepAssert.Equal(userDetail, expectedDetail);
    }

    [Fact]
    public async Task GetById_NonExistent_Does_Not_Throw()
    {
        // Setup
        UserDetailModel userDetail = new()
        {
            Id = Guid.Parse("89F51C77-8362-4D55-9EA2-FD990C970EA4"), Name = "Anton", Surname = "Bernolak"
        };

        // Exercise
        UserDetailModel? user = await _userFacadeSUT.GetAsync(userDetail.Id, string.Empty);

        //Verify
        Assert.Null(user);
    }

    [Fact]
    public async Task Get_UsersByIdFromAll_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user1 = new() { Name = "Anton", Surname = "Bernolak" };
        UserDetailModel user2 = new() { Name = "Andrej", Surname = "Danko" };
        UserDetailModel user3 = new() { Name = "Ludovít", Surname = "Štúr" };

        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);
        UserDetailModel actualUser2 = await _userFacadeSUT.SaveAsync(user2);
        UserDetailModel actualUser3 = await _userFacadeSUT.SaveAsync(user3);

        // Exercise
        IEnumerable<UserListModel> users = await _userFacadeSUT.GetAsync();
        UserListModel returnedUser1 = users.Single(i => i.Id == actualUser1.Id);
        UserListModel returnedUser2 = users.Single(i => i.Id == actualUser2.Id);
        UserListModel returnedUser3 = users.Single(i => i.Id == actualUser3.Id);

        // Verify
        Assert.Equal(actualUser1.Id, returnedUser1.Id);
        Assert.Equal(actualUser2.Id, returnedUser2.Id);
        Assert.Equal(actualUser3.Id, returnedUser3.Id);
    }

    [Fact]
    public async Task DeleteOneUserById_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user1 = new() { Name = "Anton", Surname = "Bernolak" };
        UserDetailModel user2 = new() { Name = "Andrej", Surname = "Danko" };
        UserDetailModel user3 = new() { Name = "Ludovít", Surname = "Štúr" };

        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);
        UserDetailModel actualUser2 = await _userFacadeSUT.SaveAsync(user2);
        UserDetailModel actualUser3 = await _userFacadeSUT.SaveAsync(user3);

        // Exercise
        await _userFacadeSUT.DeleteAsync(actualUser1.Id);

        // Verify
        UserDetailModel? actualDeletedUser = await _userFacadeSUT.GetAsync(actualUser1.Id, string.Empty);
        Assert.Null(actualDeletedUser);
    }

    [Fact]
    public async Task DeleteById_DeletedUser_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user1 = new() { Name = "Anton", Surname = "Bernolak" };
        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);

        // Exercise
        await _userFacadeSUT.DeleteAsync(actualUser1.Id);

        // Verify
        await using ProjectDbContext dbxAssert = await DbContextFactory.CreateDbContextAsync();
        Assert.False(await dbxAssert.Users.AnyAsync(i => i.Id == actualUser1.Id));
    }

    [Fact]
    public async Task UpdateUser_Does_Not_Throw()
    {
        // Setup
        UserDetailModel user1 = new() { Name = "Anton", Surname = "Bernolak" };
        UserDetailModel user2 = new() { Name = "Andrej", Surname = "Danko" };
        UserDetailModel user3 = new() { Name = "Ludovít", Surname = "Štúr" };

        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);
        UserDetailModel actualUser2 = await _userFacadeSUT.SaveAsync(user2);
        UserDetailModel actualUser3 = await _userFacadeSUT.SaveAsync(user3);


        // Exercise
        UserDetailModel update = new()
        {
            Id = user3.Id,
            Name = "Martin",
            Surname = "Štúrec",
            PhotoUrl = "https://upload.wikimedia.org/wikipedia/commons/3/3b/Ludovit_Stur_head_remix.png"
        };
        UserDetailModel actualUpdate = await _userFacadeSUT.SaveAsync(update);


        // Verify
        await using ProjectDbContext dbxAssert = await DbContextFactory.CreateDbContextAsync();
        UserEntity userFromDB = await dbxAssert.Users.SingleAsync(i => i.Id == actualUpdate.Id);
        DeepAssert.Equal(actualUpdate, UserModelMapper.MapToDetailModel(userFromDB));
    }

    [Fact]
    public async Task GetOne_User_Activity_Does_Not_Throw()
    {
        UserDetailModel user = new() { Name = "Anton", Surname = "Bernolák" };
        UserDetailModel actualUser = await _userFacadeSUT.SaveAsync(user);
        ActivityDetailModel activityDetail = new()
        {
            ActivityType = "Activity",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            UserId = actualUser.Id,
            UserName = actualUser.Name,
            UserSurname = actualUser.Surname
        };
        ActivityDetailModel expectedDetail = await _activityFacadeSUT.SaveAsync(activityDetail);

        UserDetailModel? actualDetail = await _userFacadeSUT.GetAsync(actualUser.Id, "Activities");
        Assert.NotNull(actualDetail);
    }
}
