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
        var detail = new UserDetailModel()
        {
            Id = Guid.Empty, Name = @"User name", Surname = @"User surname", PhotoUrl = null
        };

        var actualDetail = await _userFacadeSUT.SaveAsync(detail);

        DeepAssert.Equal(detail, actualDetail);
    }
}
