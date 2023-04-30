using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;


[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserDetailViewModel : ViewModelBase, IRecipient<UserEditMessage>, IRecipient<UserDeleteMessage>
{
    private readonly IUserFacade _userFacade;
    private readonly INavigationService _navigationService;

    public UserDetailModel? User { get; set; }
    public Guid Id { get; set; }

    public UserDetailViewModel(IMessengerService messengerService, IUserFacade userFacade,
        INavigationService navigationService) : base(messengerService)
    {
        _userFacade = userFacade;
        _navigationService = navigationService;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, string.Empty);
    }

    [RelayCommand]
    private async Task DeleteAsync()
    {
        if (User is not null)
        {
            await _userFacade.DeleteAsync(User.Id);
            MessengerService.Send(new UserDeleteMessage());
            _navigationService.SendBackButtonPressed();
        }
    }

    [RelayCommand]
    public async Task GoToEditUser()
    {
        if (User != null)
        {
            await _navigationService.GoToAsync("/edit",
                new Dictionary<string, object?> { [nameof(UserEditViewModel.Id)] = User.Id});
        }
    }

    public async void Receive(UserEditMessage message)
    {
        if (message.UserId == User?.Id)
        {
            await LoadDataAsync();
        }
    }

    public async void Receive(UserDeleteMessage message)
    {
        await LoadDataAsync();
    }
}
