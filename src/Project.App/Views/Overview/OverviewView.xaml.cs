using Project.App.ViewModels;

namespace Project.App.Views.Overview;

public partial class OverviewView
{
    public OverviewView(OverviewViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }

    private void Button_OnClicked(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }
}
