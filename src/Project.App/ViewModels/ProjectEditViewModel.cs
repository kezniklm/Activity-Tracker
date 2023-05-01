using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;
using Windows.System;

namespace Project.App.ViewModels;

[QueryProperty(nameof(ActualProjectId), nameof(ActualProjectId))]
[QueryProperty(nameof(UserId), nameof(UserId))]

public partial class ProjectEditViewModel : ViewModelBase, IRecipient<ActivityEditMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IProjectFacade _projectFacade;
    private readonly IActivityFacade _activityFacade;

    public Guid ActualProjectId { get; set; }
    public Guid UserId { get; set; }
    public ProjectDetailModel? Project { get; set; }
    public ActivityDetailModel? Activity { get; set; } = ActivityDetailModel.Empty;
    public IEnumerable<ActivityListModel?> Activities { get; set; }

    public ProjectEditViewModel(INavigationService navigationService,
        IMessengerService messengerService, IProjectFacade projectFacade,
        IActivityFacade activityFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _projectFacade = projectFacade;
        _activityFacade = activityFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Project = await _projectFacade.GetAsync(ActualProjectId, "Activities");
        Activities = await _activityFacade.GetUserActivitiesNotInProject(UserId);
    }

    [RelayCommand]
    private async Task SaveDataAsync()
    {
        await _projectFacade.SaveAsync(Project);
        MessengerService.Send(new ProjectEditMessage(){ProjectId = Project.Id});
        _navigationService.SendBackButtonPressed();
    }

    [RelayCommand]
    private async Task DeleteFromProjectAsync(Guid RemoveId)
    {
        Activity = await _activityFacade.GetAsync(RemoveId, "User");
        Activity.ProjectId = null;
        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage() { ActivityId = Activity.Id });

    }

    [RelayCommand]
    private async Task AddToProjectAsync(Guid AddId)
    {
        Activity = await _activityFacade.GetAsync(AddId, "User");
        Activity.ProjectId = ActualProjectId;
        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage() { ActivityId = Activity.Id });
    }

    public async void Receive(ActivityEditMessage message)
    {
        await LoadDataAsync();
    }
}
