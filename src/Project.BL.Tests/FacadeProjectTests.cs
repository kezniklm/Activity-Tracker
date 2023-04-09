namespace Project.BL.Tests;

public class FacadeProjectTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;

    public FacadeProjectTests() => _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);

    [Fact]
    public async Task Create_New_Project()
    {
        // Setup
        ProjectDetailModel projectDetail = new() { Name = "Projekt1" };

        // Exercise
        ProjectDetailModel actualDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

        // Verify
        FixIds(projectDetail, actualDetail);
        DeepAssert.Equal(projectDetail, actualDetail);
    }

    [Fact]
    public async Task Create_2New_Projects_Same_Name_Throw()
    {
        // Setup
        ProjectDetailModel projectDetail1 = new() { Name = "Projekt1" };

        // Exercise
        await _projectFacadeSUT.SaveAsync(projectDetail1);

        ProjectDetailModel projectDetail2 = new() { Name = "Projekt1" };

        // Verify
        await Assert.ThrowsAnyAsync<InvalidOperationException>(() => _projectFacadeSUT.SaveAsync(projectDetail2));
    }

    [Fact]
    public async Task Get_OneProject()
    {
        // Setup
        ProjectDetailModel projectDetail = new() { Name = "Projekt1" };
        ProjectDetailModel expectedDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

        // Exercise
        ProjectDetailModel? actualDetail = await _projectFacadeSUT.GetAsync(expectedDetail.Id, string.Empty);

        // Verify
        DeepAssert.Equal(expectedDetail, actualDetail);
    }

    [Fact]
    public async Task Get_All_Projects()
    {
        // Setup
        ProjectEntity projectEntity = new() { Name = "Projekt1" };

        await using ProjectDbContext dbContext = await DbContextFactory.CreateDbContextAsync();
        dbContext.Projects.Add(projectEntity);
        await dbContext.SaveChangesAsync();

        ProjectListModel projectList = ProjectModelMapper.MapToListModel(projectEntity);

        // Exercise
        IEnumerable<ProjectListModel> actualList = await _projectFacadeSUT.GetAsync();

        // Verify
        Assert.Contains(projectList, actualList);
    }

    [Fact]
    public async Task Update_Existing_Project()
    {
        // Setup
        ProjectDetailModel projectDetail = new() { Name = "Projekt1" };
        ProjectDetailModel expectedDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

        expectedDetail.Name = "ProjektPremenovany";

        ProjectDetailModel changedDetail = await _projectFacadeSUT.SaveAsync(expectedDetail);

        DeepAssert.Equal(expectedDetail, changedDetail);
    }

    [Fact]
    public async Task Delete_Project()
    {
        // Setup
        ProjectDetailModel projectDetail = new() { Name = "Projekt1" };
        ProjectDetailModel expectedDetail = await _projectFacadeSUT.SaveAsync(projectDetail);

        // Exercise
        await _projectFacadeSUT.DeleteAsync(expectedDetail.Id);

        // Verify
        ProjectDetailModel? actualDetail = await _projectFacadeSUT.GetAsync(expectedDetail.Id, string.Empty);

        Assert.Null(actualDetail);
    }
}
