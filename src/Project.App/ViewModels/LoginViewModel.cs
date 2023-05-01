using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

public partial class LoginViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public LoginViewModel(
        INavigationService navigationService,
        IUserFacade userFacade,
        IMessengerService messengerService) : base(messengerService)
    {
        _navigationService = navigationService;
        _userFacade = userFacade;
    }

    public List<UserListModel> Users { get; set; } = null!;
    public UserListModel? SelectedUser { get; set; } = UserListModel.Empty;

    public async void Receive(UserDeleteMessage message) => await LoadDataAsync();

    public async void Receive(UserEditMessage message) => await LoadDataAsync();

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        IEnumerable<UserListModel> users = await _userFacade.GetAsync();
        Users = users.ToList();
    }

    [RelayCommand]
    public async Task GoToOverviewAsync()
    {
        if (SelectedUser != null)
        {
            Id = SelectedUser.Id;
            MessengerService.Send(new UserLoginMessage { UserId = SelectedUser.Id });
            await _navigationService.GoToAsync<OverviewViewModel>(
                new Dictionary<string, object?> { [nameof(Id)] = SelectedUser.Id });
        }
    }

    [RelayCommand]
    public async Task GoToCreateUserAsync() => await _navigationService.GoToAsync("/edit");

    [RelayCommand]
    public async Task GoToUserDetailAsync()
    {
        if (SelectedUser != null)
        {
            Id = SelectedUser.Id;
            await _navigationService.GoToAsync<UserDetailViewModel>(
                new Dictionary<string, object?> { [nameof(Id)] = SelectedUser.Id });
        }
    }

    [RelayCommand]
    public async Task GoToActivityListAsync()
    {
        if (SelectedUser != null)
        {
            await _navigationService.GoToAsync<ActivityListViewModel>(
                new Dictionary<string, object?> { [nameof(Id)] = SelectedUser.Id });
        }
    }
}
