using CommunityToolkit.Mvvm.Input;
using Project.App.Messages;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

[QueryProperty(nameof(Id), nameof(Id))]
public partial class UserEditViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public UserDetailModel User { get; set; } = UserDetailModel.Empty;
    public Guid Id { get; set; }

    public UserEditViewModel(IMessengerService messengerService, INavigationService navigationService, IUserFacade userFacade) : base(messengerService)
    {
        _navigationService = navigationService;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        User = await _userFacade.GetAsync(Id, string.Empty) ?? UserDetailModel.Empty;
    }

    [RelayCommand]
    private async Task SaveUserAsync()
    {
        await _userFacade.SaveAsync(User);
        MessengerService.Send(new UserEditMessage(){UserId = User.Id});
        _navigationService.SendBackButtonPressed();
    }
}
