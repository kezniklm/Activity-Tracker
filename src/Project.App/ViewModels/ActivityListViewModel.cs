using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public class ActivityListViewModel : ViewModelBase, IRecipient<UserLoginMessage>
{
    private readonly INavigationService _navigationService;
    public DateTime StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan EndTime { get; set; }

    public Guid Id { get; set; }

    public UserDetailModel? User { get; set; }

    private readonly IActivityFacade _activityFacade;
    private readonly IUserFacade _userFacade;

    public ObservableCollection<ActivityListModel> ListOfActivities { get; set; } = null!;

    public ActivityListViewModel(INavigationService navigationService,
        IMessengerService messengerService, IActivityFacade activityFacade, IUserFacade userFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id,"Activities");
        if (User != null)
        {
            ListOfActivities = User.Activities;
        }
    }

    public async void Receive(UserLoginMessage message)
    {
        Id = message.UserId;
        await LoadDataAsync();
    }
}
