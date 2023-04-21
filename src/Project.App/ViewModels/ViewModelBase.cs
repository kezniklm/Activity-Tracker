namespace Project.App.ViewModels;

public class ViewModelBase : IViewModel
{
    private bool _isRefreshRequired = true;

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
