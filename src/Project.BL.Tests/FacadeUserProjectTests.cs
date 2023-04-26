namespace Project.BL.Tests;

public class FacadeUserProjectTests : FacadeTestsBase
{
    private readonly IProjectFacade _projectFacadeSUT;
    private readonly IUserFacade _userFacadeSUT;
    private readonly IUserProjectFacade _userProjectFacadeSUT;

    public FacadeUserProjectTests()
    {
        _userProjectFacadeSUT = new UserProjectFacade(UnitOfWorkFactory, UserProjectModelMapper);
        _userFacadeSUT = new UserFacade(UnitOfWorkFactory, UserModelMapper);
        _projectFacadeSUT = new ProjectFacade(UnitOfWorkFactory, ProjectModelMapper);
    }

    [Fact]
    public async Task Create_New_UserProject_Does_Not_Throw()
    {
        //Setup
        UserDetailModel user = new() { Name = "John", Surname = "Doe" };
        UserDetailModel newUser = await _userFacadeSUT.SaveAsync(user);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser.Id };

        //Exercise
        UserProjectDetailModel expectedUserProject = await _userProjectFacadeSUT.SaveAsync(userProject);
        FixIds(expectedUserProject, userProject);

        //Verify
        DeepAssert.Equal(expectedUserProject, userProject);
    }

    [Fact]
    public async Task Delete_UserProject_Does_Not_Throw()
    {
        //Setup
        UserDetailModel user = new() { Name = "John", Surname = "Doe" };
        UserDetailModel newUser = await _userFacadeSUT.SaveAsync(user);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser.Id };
        UserProjectDetailModel expectedUserProject = await _userProjectFacadeSUT.SaveAsync(userProject);
        FixIds(expectedUserProject, userProject);

        //Exercise
        await _userProjectFacadeSUT.DeleteAsync(expectedUserProject.Id);

        //Verify
        ProjectDetailModel? actualDetail = await _projectFacadeSUT.GetAsync(expectedUserProject.Id, string.Empty);
        Assert.Null(actualDetail);
    }

    [Fact]
    public async Task Get_UserProject_By_Id_Does_Not_Throw()
    {
        //Setup
        UserDetailModel user = new() { Name = "John", Surname = "Doe" };
        UserDetailModel newUser = await _userFacadeSUT.SaveAsync(user);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser.Id };
        UserProjectDetailModel expectedUserProject = await _userProjectFacadeSUT.SaveAsync(userProject);
        FixIds(expectedUserProject, userProject);

        //Exercise
        UserProjectDetailModel? actualUserProject =
            await _userProjectFacadeSUT.GetAsync(expectedUserProject.Id, string.Empty);

        //Verify
        DeepAssert.Equal(expectedUserProject, actualUserProject);
    }

    [Fact]
    public async Task Update_UserProject_Does_Not_Throw()
    {
        //Setup
        UserDetailModel user = new() { Name = "John", Surname = "Doe" };
        UserDetailModel newUser = await _userFacadeSUT.SaveAsync(user);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser.Id };
        UserProjectDetailModel expectedUserProject = await _userProjectFacadeSUT.SaveAsync(userProject);
        FixIds(expectedUserProject, userProject);

        UserProjectDetailModel updatedUserProject =
            new() { Id = expectedUserProject.Id, ProjectId = newProject.Id, UserId = newUser.Id };

        //Exercise
        await _userProjectFacadeSUT.SaveAsync(updatedUserProject);
        UserProjectDetailModel? actualUserProject =
            await _userProjectFacadeSUT.GetAsync(expectedUserProject.Id, string.Empty);

        //Verify
        DeepAssert.Equal(updatedUserProject, actualUserProject);
    }

    [Fact]
    public async Task Get_One_UserProject_Does_Not_Throw()
    {
        // Setup

        UserDetailModel user = new() { Name = "John", Surname = "Doe" };
        UserDetailModel newUser = await _userFacadeSUT.SaveAsync(user);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser.Id };
        UserProjectDetailModel expectedUserProject = await _userProjectFacadeSUT.SaveAsync(userProject);
        FixIds(expectedUserProject, userProject);


        // Exercise
        UserProjectDetailModel? actualDetail =
            await _userProjectFacadeSUT.GetAsync(expectedUserProject.Id, string.Empty);

        // Verify
        DeepAssert.Equal(expectedUserProject, actualDetail);
    }

    [Fact]
    public async Task Create_2Users_With_Same_Project_Does_Not_Throw()
    {
        //Setup
        UserDetailModel user1 = new() { Name = "John", Surname = "Doe" };
        UserDetailModel user2 = new() { Name = "Jane", Surname = "Doe" };
        UserDetailModel newUser1 = await _userFacadeSUT.SaveAsync(user1);
        UserDetailModel newUser2 = await _userFacadeSUT.SaveAsync(user2);

        ProjectDetailModel project = new() { Name = "Projekt1" };
        ProjectDetailModel newProject = await _projectFacadeSUT.SaveAsync(project);

        UserProjectDetailModel userProject1 =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser1.Id };
        UserProjectDetailModel userProject2 =
            new() { Id = Guid.NewGuid(), ProjectId = newProject.Id, UserId = newUser2.Id };

        //Exercise
        UserProjectDetailModel expectedUserProject1 = await _userProjectFacadeSUT.SaveAsync(userProject1);
        UserProjectDetailModel expectedUserProject2 = await _userProjectFacadeSUT.SaveAsync(userProject2);

        //Verify
        Assert.Equal(newProject.Id, expectedUserProject1.ProjectId);
        Assert.Equal(newUser1.Id, expectedUserProject1.UserId);
        Assert.Equal(newProject.Id, expectedUserProject2.ProjectId);
        Assert.Equal(newUser2.Id, expectedUserProject2.UserId);
    }
}
