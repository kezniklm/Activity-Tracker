using Project.App.ViewModels;

namespace Project.App.Views.Overview;

public partial class ActivityEditView : ContentPageBase
{
	public ActivityEditView(ActivityEditViewModel viewModel) : base(viewModel)
	{
		InitializeComponent();
	}
}
