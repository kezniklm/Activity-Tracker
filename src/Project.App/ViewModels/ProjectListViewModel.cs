using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectListViewModel : ViewModelBase, IRecipient<UserLoginMessage>,
    IRecipient<ProjectCreateMessage>, IRecipient<ProjectEditMessage>, IRecipient<LogOutFromProjectMessage>,
    IRecipient<JoinProjectMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IUserProjectFacade _userProjectFacade;

    public ProjectListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade, IUserFacade userFacade,
        IUserProjectFacade userProjectFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _userProjectFacade = userProjectFacade;
    }

    public UserDetailModel? User { get; set; }
    public IEnumerable<ProjectListModel>? MyProjects { get; set; }
    public IEnumerable<ProjectListModel>? OtherProjects { get; set; }

    public async void Receive(JoinProjectMessage message) => await LoadDataAsync();

    public async void Receive(LogOutFromProjectMessage message) => await LoadDataAsync();

    public async void Receive(ProjectCreateMessage message) => await LoadDataAsync();

    public async void Receive(ProjectEditMessage message) => await LoadDataAsync();

    public async void Receive(UserLoginMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, "Projects");

        Tuple<IEnumerable<ProjectListModel?>, IEnumerable<ProjectListModel?>> projectsList =
            await _userProjectFacade.DisplayProjectsOfUser(Id);
        MyProjects = projectsList.Item1;
        OtherProjects = projectsList.Item2;
    }

    [RelayCommand]
    public async Task GoToCreateProjectAsync() =>
        await _navigationService.GoToAsync("/create",
            new Dictionary<string, object?> { [nameof(Id)] = Id });

    [RelayCommand]
    public async Task GotoEditProjectAsync(Guid selectedProjectID) =>
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object?>
            {
                [nameof(ProjectEditViewModel.ActualProjectId)] = selectedProjectID, [nameof(Id)] = Id
            });

    [RelayCommand]
    public async Task LogOutFromProjectAsync(Guid selectedProjectID)
    {
        UserProjectDetailModel? UserProject = await _userProjectFacade.GetUserProjectByIds(Id, selectedProjectID);
        await _userProjectFacade.DeleteAsync(UserProject.Id);
        MessengerService.Send(new LogOutFromProjectMessage());
    }

    [RelayCommand]
    public async Task JoinProjectAsync(Guid selectedProjectID)
    {
        UserProjectDetailModel newUserProject = new() { ProjectId = selectedProjectID, UserId = Id };
        await _userProjectFacade.SaveAsync(newUserProject);
        MessengerService.Send(new JoinProjectMessage());
    }
}
