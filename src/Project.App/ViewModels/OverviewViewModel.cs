using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class OverviewViewModel : ViewModelBase, IRecipient<UserLoginMessage>, IRecipient<ActivityEditMessage>,
    IRecipient<ActivityDeleteMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;


    public OverviewViewModel(
        INavigationService navigationService,
        IUserFacade userFacade,
        IMessengerService messengerService) : base(messengerService)
    {
        _navigationService = navigationService;
        _userFacade = userFacade;
    }


    public UserDetailModel? User { get; set; }

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
    }

    [RelayCommand]
    public async Task CreateActivityAsync() => await _navigationService.GoToAsync("/edit",
        new Dictionary<string, object?> { [nameof(Id)] = Id });

    [RelayCommand]
    public async Task EditActivityAsync(Guid SelectedActivityId) => await _navigationService.GoToAsync("/edit",
        new Dictionary<string, object?>
        {
            [nameof(ActivityEditViewModel.ActivityId)] = SelectedActivityId, [nameof(Id)] = Id
        });
}
