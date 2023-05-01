using System.Xml.Linq;
using Project.App.ViewModels;

namespace Project.App.Views.Overview;

[QueryProperty(nameof(Meno), nameof(Meno))]
public partial class OverviewView
{
    private string _meno;

    public string Meno
    {
        get => _meno;
        set => _meno = value;
    }

    public OverviewView(OverviewViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    protected override async void OnAppearing()
    {
        await ViewModel.OnAppearingAsync();
        base.OnAppearing();
    }
}
