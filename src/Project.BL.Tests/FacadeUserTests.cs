using Project.BL.Facades;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;
using Project.Common.Tests;

namespace Project.BL.Tests;

public class FacadeUserTests : FacadeTestsBase
{
    private readonly IUserFacade _userFacadeSUT;

    public FacadeUserTests()
    {
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
    }

    [Fact]
    public async Task Create_WithNonExistingItem_DoesNotThrow()
    {
        var userDetail = new UserDetailModel()
        {
            Name = @"User name", Surname = @"User surname", PhotoUrl = null
        };

        var expectedDetail = await _userFacadeSUT.SaveAsync(userDetail);
        FixIds(expectedDetail, userDetail);
        DeepAssert.Equal(userDetail, expectedDetail);
    }
}
