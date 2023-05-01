using Project.App.ViewModels;

namespace Project.App.Views.Overview;

public partial class OverviewView : ContentPageBase
{
    public OverviewView(OverviewViewModel viewModel) : base(viewModel) => InitializeComponent();
}
