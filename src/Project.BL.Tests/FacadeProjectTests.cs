using Project.BL.Facades.Interfaces;
using Project.BL.Facades;
using Project.BL.Models;
using Project.Common.Tests;
using Project.DAL.Entities;

namespace Project.BL.Tests;
public class FacadeProjectTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;

    public FacadeProjectTests()
    {
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_New_Project()
    {
        // Setup
        ProjectDetailModel projectDetail = new()
        {
            Id = Guid.NewGuid(),
            Name = "Projekt1"
        };

        // Exercise
        ProjectDetailModel actualDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

        // Verify
        FixIds(projectDetail, actualDetail);
        DeepAssert.Equal(projectDetail, actualDetail);
    }

    //[Fact]
    //public async Task Get_OneProject()
    //{
    //    // Setup
    //    ProjectDetailModel projectDetail = new()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Projekt1"
    //    };
    //    ProjectDetailModel expectedDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

    //    // Exercise
    //    ProjectDetailModel? actualDetail = await _projectFacadeSUT.GetAsync(expectedDetail.Id);

    //    // Verify
    //    DeepAssert.Equal(expectedDetail, actualDetail);
    //}

    [Fact]
    public async Task Get_All_Projects()
    {
        // Setup
        ProjectEntity projectEntity = new()
        {
            Id = Guid.NewGuid(),
            Name = "Projekt1"
        };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Projects.Add(projectEntity);
        await dbContext.SaveChangesAsync();

        ProjectListModel projectList = ProjectModelMapper.MapToListModel(projectEntity);

        // Exercise
        IEnumerable<ProjectListModel> actualList = await _projectFacadeSUT.GetAsync();

        // Verify
        Assert.Contains(projectList, actualList);
    }

    //[Fact]
    //public async Task Delete_Project()
    //{
    //    // Setup
    //    ProjectDetailModel projectDetail = new()
    //    {
    //        Id = Guid.NewGuid(),
    //        Name = "Projekt1"
    //    };
    //    ProjectDetailModel expectedDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

    //    // Exercise
    //    await _projectFacadeSUT.DeleteAsync(expectedDetail.Id);

    //    // Verify
    //    ProjectDetailModel? actualDetail = await _projectFacadeSUT.GetAsync(expectedDetail.Id);

    //    Assert.Null(actualDetail);
    //}

    private static void FixIds(ModelBase expectedDetail, ModelBase actualDetail)
    {
        actualDetail.Id = expectedDetail.Id;
    }
}
