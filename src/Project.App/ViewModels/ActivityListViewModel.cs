using Project.App.Services;

namespace Project.App.ViewModels;

public partial class ActivityListViewModel : ViewModelBase
{
    private readonly INavigationService _navigationService;

    public ActivityListViewModel(INavigationService navigationService,
        IMessengerService messengerService) : base(messengerService)
    {
        _navigationService = navigationService;
    }
}
