using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityListViewModel : ViewModelBase, IRecipient<UserLoginMessage>, IViewModel
{
    private readonly IActivityFacade _activityFacade;
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public ActivityListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IActivityFacade activityFacade, IUserFacade userFacade) : base(
        messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
        _userFacade = userFacade;
    }

    public DateTime StartDate { get; set; } = DateTime.Today;
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; } = DateTime.Today;
    public TimeSpan EndTime { get; set; }

    public UserDetailModel? User { get; set; }

    public IEnumerable<ActivityListModel> ListOfActivities { get; set; } = null!;

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
        ListOfActivities = await _activityFacade.FilterLastMonth(Id);
        await base.LoadDataAsync();
    }

    [RelayCommand]
    public async Task ResetFilters() => await LoadDataAsync();
}
