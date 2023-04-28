using Project.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Project.App.ViewModels;

public abstract class ViewModelBase : ObservableRecipient, IViewModel
{
    private bool _isRefreshRequired = true;
    protected readonly IMessengerService MessengerService;

    protected ViewModelBase(IMessengerService messengerService) : base(messengerService.Messenger)
    {
        MessengerService = messengerService;
        IsActive = true;
    }

    public async Task OnAppearingAsync()
    {
        if (_isRefreshRequired)
        {
            await LoadDataAsync();

            _isRefreshRequired = false;
        }
    }

    public virtual Task LoadDataAsync()
        => Task.CompletedTask;
}
