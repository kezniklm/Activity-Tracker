using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityListViewModel : ViewModelBase, IRecipient<UserLoginMessage>,
    IRecipient<ActivityEditMessage>,
    IRecipient<ActivityDeleteMessage>
{
    private readonly IActivityFacade _activityFacade;
    private readonly IAlertService _alertService;
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public ActivityListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IActivityFacade activityFacade, IUserFacade userFacade,
        IAlertService alertService) : base(
        messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
        _userFacade = userFacade;
        _alertService = alertService;
    }

    public DateTime StartDate { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; } = DateTime.Today;
    public TimeSpan EndTime { get; set; }
    private DateTime FilterStart { get; set; }
    private DateTime FilterEnd { get; set; }


    public UserDetailModel? User { get; set; }

    public IEnumerable<ActivityListModel> ListOfActivities { get; set; } = null!;

    public async void Receive(ActivityDeleteMessage message) => await LoadDataAsync();

    public async void Receive(ActivityEditMessage message) => await LoadDataAsync();

    public async void Receive(UserLoginMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, "Activities");
        if (User != null)
        {
            ListOfActivities = User.Activities;
        }
    }

    [RelayCommand]
    public async Task ActivityFilterThisWeek()
    {
        ListOfActivities = await _activityFacade.FilterThisWeek(Id);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ActivityFilterThisMonth()
    {
        ListOfActivities = await _activityFacade.FilterThisMonth(Id);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ActivityFilterThisYear()
    {
        ListOfActivities = await _activityFacade.FilterThisYear(Id);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ActivityFilterLastMonth()
    {
        ListOfActivities = await _activityFacade.FilterLastMonth(Id);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ActivityFilterByDate()
    {
        FilterStart = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours,
            StartTime.Minutes, StartTime.Seconds);
        FilterEnd = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes,
            EndTime.Seconds);
        try
        {
            ListOfActivities = await _activityFacade.Filter(FilterStart, FilterEnd, Id);
        }
        catch (Exception ex)
        {
            await _alertService.DisplayAsync("Activity Error", ex.Message);
        }

        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ResetFilters()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today;
        StartTime = TimeSpan.Zero;
        EndTime = TimeSpan.Zero;
        await LoadDataAsync();
    }

    [RelayCommand]
    public async Task EditActivity(Guid SelectedActivityId) =>
        await _navigationService.GoToAsync<ActivityEditViewModel>(
            new Dictionary<string, object?>
            {
                [nameof(ActivityEditViewModel.ActivityId)] = SelectedActivityId, [nameof(Id)] = Id
            });
}
