using Project.App.ViewModels;

namespace Project.App.Views;

public partial class ContentPageBase
{
    public ContentPageBase(IViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = ViewModel = viewModel;
    }

    protected IViewModel ViewModel { get; }

    protected override async void OnAppearing()
    {
        await ViewModel.OnAppearingAsync();
        base.OnAppearing();
    }
}
