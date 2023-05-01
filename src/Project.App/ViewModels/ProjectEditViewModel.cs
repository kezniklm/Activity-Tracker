using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(ActualProjectId), nameof(ActualProjectId))]
[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;
    private readonly IUserProjectFacade _userProjectFacade;
    private readonly IUserFacade _userFacade;

    public ProjectEditViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade,
        IActivityFacade activityFacade, IUserProjectFacade userProjectFacade, IUserFacade userFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
        _userProjectFacade = userProjectFacade;
        _userFacade = userFacade;
    }

    public Guid ActualProjectId { get; set; }

    public ProjectDetailModel? Project { get; set; }
    public ActivityDetailModel? Activity { get; set; } = ActivityDetailModel.Empty;
    public IEnumerable<ActivityListModel?> Activities { get; set; }

    public async void Receive(ActivityEditMessage message) => await LoadDataAsync();

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = await _projectFacade.GetAsync(ActualProjectId, "Activities");
        Activities = await _activityFacade.GetUserActivitiesNotInProject(Id);
    }

    [RelayCommand]
    private async Task SaveDataAsync()
    {
        await _projectFacade.SaveAsync(Project);
        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteProjectAsync()
    {
        //UserProjectDetailModel? UserProject = await _userProjectFacade.GetUserProjectByIds(UserId, ActualProjectId);
        //await _userProjectFacade.DeleteAsync(UserProject.Id);
        
        await _projectFacade.DeleteAsync(Project.Id);
        MessengerService.Send(new ProjectDeleteMessage());
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteFromProjectAsync(Guid RemoveId)
    {
        Activity = await _activityFacade.GetAsync(RemoveId, "User");
        Activity.ProjectId = null;
        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
    }

    [RelayCommand]
    private async Task AddToProjectAsync(Guid AddId)
    {
        Activity = await _activityFacade.GetAsync(AddId, "User");
        Activity.ProjectId = ActualProjectId;
        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
    }
}
