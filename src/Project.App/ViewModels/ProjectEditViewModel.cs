using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(ActualProjectId), nameof(ActualProjectId))]
[QueryProperty(nameof(Id), nameof(Id))]
public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;

    public ProjectEditViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade,
        IActivityFacade activityFacade, IAlertService alertService) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
        _alertService = alertService;
    }

    public Guid ActualProjectId { get; set; }
    public ProjectDetailModel Project { get; set; } = null!;
    public ActivityDetailModel? Activity { get; set; } = ActivityDetailModel.Empty;
    public IEnumerable<ActivityListModel?> Activities { get; set; } = null!;

    public async void Receive(ActivityEditMessage message) => await LoadDataAsync();

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = (await _projectFacade.GetAsync(ActualProjectId, "Activities"))!;
        Activities = await _activityFacade.GetUserActivitiesNotInProject(Id);
    }

    [RelayCommand]
    private async Task SaveDataAsync()
    {
        try
        {
            await _projectFacade.SaveAsync(Project);
        }
        catch (Exception ex)
        {
            await _alertService.DisplayAsync("Project Error", ex.Message);
        }

        MessengerService.Send(new ProjectEditMessage { ProjectId = Project.Id });
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteProjectAsync()
    {
        await _projectFacade.DeleteAsync(Project.Id);
        MessengerService.Send(new ProjectDeleteMessage());
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteFromProjectAsync(Guid removeId)
    {
        Activity = await _activityFacade.GetAsync(removeId, "User");
        if (Activity != null)
        {
            Activity.ProjectId = null;
            await _activityFacade.SaveAsync(Activity);
            MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        }
    }

    [RelayCommand]
    private async Task AddToProjectAsync(Guid addId)
    {
        Activity = await _activityFacade.GetAsync(addId, "User");
        if (Activity != null)
        {
            Activity.ProjectId = ActualProjectId;
            await _activityFacade.SaveAsync(Activity);
            MessengerService.Send(new ActivityEditMessage { ActivityId = Activity.Id });
        }
    }
}
