using Project.App.Services;

namespace Project.App;

public partial class AppShell : Shell
{
    private readonly INavigationService _navigationService;

    public AppShell(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }
}
