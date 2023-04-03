using Project.BL.Facades.Interfaces;
using Project.BL.Facades;
using Project.BL.Models;
using Project.Common.Tests;

namespace Project.BL.Tests;

public class FacadeActivityTests : FacadeTestsBase
{
    private readonly IActivityFacade _activityFacadeSUT;

    public FacadeActivityTests(ITestOutputHelper output) : base(output)
    {
        _activityFacadeSUT = new ActivityFacade(UnitOfWorkFactory, ActivityModelMapper);
    }

    [Fact(Skip = "Testing")]
    public async Task Filter_test()
    {
        var activityDetail_1 = new ActivityDetailModel()
        {
            Id = Guid.Empty,
            ActivityType = "running",
            Start = new DateTime(2023, 3, 20, 15, 0, 0),
            End = new DateTime(2023, 3, 20, 16, 0, 0),
            User = new() {Id = Guid.Empty, Name = "Name", Surname = "surname"}
        };

        var actualDetail_1 = await _activityFacadeSUT.SaveAsync(activityDetail_1);

        DeepAssert.Equal(actualDetail_1, activityDetail_1);
    }
}
