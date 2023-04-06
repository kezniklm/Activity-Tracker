using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using Project.BL.Facades;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;
using Project.Common.Tests;
using Project.BL.Mappers;

namespace Project.BL.Tests;

public class FacadeUserTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public FacadeUserTests() => _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        // Setup
        UserDetailModel detail = new() { Id = new Guid(), Name = "Anton", Surname = "Bernolak" };

        // Exercise
        UserDetailModel actualDetail = await _userFacadeSUT.SaveAsync(detail);

        FixIds(detail, actualDetail);

        // Verify
        DeepAssert.Equal(detail, actualDetail);
    }

    private static void FixIds(ModelBase expectedDetail, ModelBase actualDetail) => actualDetail.Id = expectedDetail.Id;


    [Fact]
    public async Task GetById_NonExistent()
    {
        // Setup
        UserDetailModel userDetail = new() { Id = new Guid(), Name = "Anton", Surname = "Bernolak" };

        // Exercise & Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _userFacadeSUT.GetAsync(userDetail.Id));
    }

    [Fact]
    public async Task Get_UserByIdFromAll()
    {
        // Setup
        UserDetailModel user1 = new() { Id = Guid.NewGuid(), Name = "Anton", Surname = "Bernolak" };
        UserDetailModel user2 = new() { Id = Guid.NewGuid(), Name = "Andrej", Surname = "Danko" };
        UserDetailModel user3 = new() { Id = Guid.NewGuid(), Name = "Ludovít", Surname = "Štúr" };

        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);
        FixIds(user1, actualUser1);
        UserDetailModel actualUser2 = await _userFacadeSUT.SaveAsync(user2);
        FixIds(user2, actualUser2);
        UserDetailModel actualUser3 = await _userFacadeSUT.SaveAsync(user3);
        FixIds(user3, actualUser3);

        // Exercise
        IEnumerable<UserListModel> users = await _userFacadeSUT.GetAsync();
        UserListModel user = users.Single(i => i.Name == user2.Name);

        // Verify
        DeepAssert.Equal(user2.Name, user.Name);
    }

    [Fact(Skip = "Nevieme či funguje")]

public async Task DeleteOneUserById()
    {
        UserDetailModel user1 = new() { Id = Guid.NewGuid(), Name = "Anton", Surname = "Bernolak" };
        UserDetailModel user2 = new() { Id = Guid.NewGuid(), Name = "Andrej", Surname = "Danko" };
        UserDetailModel user3 = new() { Id = Guid.NewGuid(), Name = "Ludovít", Surname = "Štúr" };

        UserDetailModel actualUser1 = await _userFacadeSUT.SaveAsync(user1);

        UserDetailModel actualUser2 = await _userFacadeSUT.SaveAsync(user2);
        
        UserDetailModel actualUser3 = await _userFacadeSUT.SaveAsync(user3);
        

        // Exercise
        await _userFacadeSUT.DeleteAsync(actualUser1.Id);

        // Verify
        UserDetailModel? actualDeletedUser = await _userFacadeSUT.GetAsync(actualUser3.Id);
        Assert.Null(actualDeletedUser);
    }
}
