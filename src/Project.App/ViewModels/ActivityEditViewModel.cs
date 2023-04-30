using CommunityToolkit.Mvvm.Input;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class ActivityEditViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IActivityFacade _activityFacade;
    private readonly IUserFacade _userFacade;

    public Guid Id { get; set; }

    public DateTime StartDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public DateTime EndDate { get; set; }
    public TimeSpan EndTime { get; set; }

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(IMessengerService messengerService, INavigationService navigationService, IActivityFacade activityFacade, IUserFacade userFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
        _userFacade = userFacade;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        Activity.Start = new DateTime(StartDate.Year, StartDate.Month, StartDate.Day, StartTime.Hours,
            StartTime.Minutes, StartTime.Seconds);
        Activity.End = new DateTime(EndDate.Year, EndDate.Month, EndDate.Day, EndTime.Hours, EndTime.Minutes,
            EndTime.Seconds);


        var User = await _userFacade.GetAsync(Id, String.Empty);
        Activity.UserId = User.Id;
        Activity.UserName = User.Name;
        Activity.UserSurname = User.Surname;
        

        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage() { ActivityId = Activity.Id });
        _navigationService.SendBackButtonPressed();
    }
}
