using CommunityToolkit.Mvvm.ComponentModel;
using Project.App.Services;

namespace Project.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    protected readonly IMessengerService MessengerService;
    private bool _isRefreshRequired = true;

    protected ViewModelBase(IMessengerService messengerService) : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public static Guid Id { get; set; }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }

    protected virtual Task LoadDataAsync()
        => Task.CompletedTask;
}
