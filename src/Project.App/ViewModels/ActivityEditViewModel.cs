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

    public Guid Id { get; set; }

    public ActivityDetailModel Activity { get; init; } = ActivityDetailModel.Empty;

    public ActivityEditViewModel(IMessengerService messengerService, INavigationService navigationService, IActivityFacade activityFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _activityFacade = activityFacade;
    }

    [RelayCommand]
    public async Task SaveAsync()
    {
        Activity.UserId = Id;
        await _activityFacade.SaveAsync(Activity);
        MessengerService.Send(new ActivityEditMessage(){ActivityId = Activity.Id});
        _navigationService.SendBackButtonPressed();
    }
}
