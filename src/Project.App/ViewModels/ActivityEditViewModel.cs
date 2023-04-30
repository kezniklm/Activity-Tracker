using CommunityToolkit.Mvvm.Input;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
[QueryProperty(nameof(ActivityId), nameof(ActivityId))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IActivityFacade _activityFacade;
    private readonly IUserFacade _userFacade;
    private readonly IUserProjectFacade _userProjectFacade;

    public Guid Id { get; set; }

    public Guid ActivityId { get; set; } 

    public DateTime StartDate { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; } = DateTime.Today;
    public TimeSpan EndTime { get; set; } 

    public List<ProjectListModel> Projects { get; set; } = null!;
    public ProjectListModel? SelectedProject { get; set; } = ProjectListModel.Empty;

    public ActivityDetailModel? Activity { get; set; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(
        IMessengerService messengerService,
        INavigationService navigationService,
        IActivityFacade activityFacade,
        IUserFacade userFacade,
        IUserProjectFacade userProjectFacade ) : base(messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
        _userFacade = userFacade;
        _userProjectFacade = userProjectFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var projects = await _userProjectFacade.DisplayProjectsOfUser(Id);
        Projects = projects.Item1.ToList();

        if (ActivityId != Guid.Empty)
        {
            Activity = await _activityFacade.GetAsync(ActivityId, "User");
        }
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        Activity.Start = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours,
            StartTime.Minutes, StartTime.Seconds);
        Activity.End = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes,
            EndTime.Seconds);


        var user = await _userFacade.GetAsync(Id, String.Empty);
        Activity.UserId = user.Id;
        Activity.UserName = user.Name;
        Activity.UserSurname = user.Surname;

        if (SelectedProject != null)
        {
            Activity.ProjectId = SelectedProject.Id;
        }
        else
        {
            Activity.ProjectId = null;
        }

        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage() { ActivityId = Activity.Id });
        _navigationService.SendBackButtonPressed();
    }
}
