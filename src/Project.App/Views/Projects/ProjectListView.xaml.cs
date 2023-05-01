using Project.App.ViewModels;

namespace Project.App.Views.Projects;

public partial class ProjectListView : ContentPageBase
{
    public ProjectListView(ProjectListViewModel viewModel) : base(viewModel) => InitializeComponent();
}
