using CommunityToolkit.Mvvm.Input;
using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

public partial class LoginViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public List<UserListModel> Users { get; set; } = null!;

    public LoginViewModel(
        INavigationService navigationService,
        IUserFacade userFacade,
        IMessengerService messengerService) : base(messengerService)
    {
        _navigationService = navigationService;
        _userFacade = userFacade;
        LoadDataAsync();
    }

    public override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        var users = await _userFacade.GetAsync();
        Users = users.ToList();
    }
}
