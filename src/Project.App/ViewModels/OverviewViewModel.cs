using Project.App.Services;

namespace Project.App.ViewModels;

public partial class OverviewViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public OverviewViewModel(INavigationService navigationService) => _navigationService = navigationService;
}
