using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectListViewModel : ViewModelBase, IRecipient<UserLoginMessage>, IRecipient<ProjectCreateMessage>, IRecipient<ProjectEditMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;
    private readonly IUserFacade _userFacade;
    private readonly IUserProjectFacade _userProjectFacade;

    public Guid Id { get; set; }
    public UserDetailModel? User { get; set; }
    public IEnumerable<ProjectListModel>? MyProjects { get; set; }
    public IEnumerable<ProjectListModel>? OtherProjects { get; set; }

    public ProjectListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade, IUserFacade userFacade, IUserProjectFacade userProjectFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _userFacade = userFacade;
        _userProjectFacade = userProjectFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, "Projects");
        MyProjects = await _userProjectFacade.DisplayProjectsOfUser(Id);
        OtherProjects = await _userProjectFacade.DisplayOtherProjectsForUser(Id);
    }

    public async void Receive(UserLoginMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task GoToCreateProjectAsync()
    {
        await _navigationService.GoToAsync( "/create",
            new Dictionary<string, object?> { [nameof(ProjectCreateViewModel.Id)] = Id });
    }

    [RelayCommand]
    public async Task GotoEditProjectAsync(Guid selectedProjectID)
    {
        await _navigationService.GoToAsync("/edit",
            new Dictionary<string, object?> { [nameof(ProjectEditViewModel.ActualProjectId)] = selectedProjectID });
    }

    public async void Receive(ProjectCreateMessage message)
    {
        await LoadDataAsync();
    }

    public async void Receive(ProjectEditMessage message)
    {
        await LoadDataAsync();
    }
}
