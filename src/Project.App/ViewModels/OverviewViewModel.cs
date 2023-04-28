using Project.App.Services;
using Project.BL.Facades.Interfaces;
using Project.BL.Models;

namespace Project.App.ViewModels;

public partial class OverviewViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;
    private readonly IUserFacade _userFacade;

    public IEnumerable<UserListModel> Users { get; set; } = null!;

    public OverviewViewModel(INavigationService navigationService, IUserFacade userFacade)
    {
        _navigationService = navigationService;
        _userFacade = userFacade;
    }

    protected override async Task LoadDataAsync()
    {
        await base.LoadDataAsync();
        Users = await _userFacade.GetAsync();
    }
}
