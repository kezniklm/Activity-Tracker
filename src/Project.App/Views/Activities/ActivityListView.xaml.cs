using Project.App.ViewModels;

namespace Project.App.Views.Activities;

public partial class ActivityListView : ContentPageBase
{
	public ActivityListView(ActivityListViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}
