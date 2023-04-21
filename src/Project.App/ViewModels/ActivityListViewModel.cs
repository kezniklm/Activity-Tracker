using Project.App.Services;

namespace Project.App.ViewModels;

public partial class ActivityListViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public ActivityListViewModel(INavigationService navigationService) => _navigationService = navigationService;
}
