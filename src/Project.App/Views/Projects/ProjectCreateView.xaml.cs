using Project.App.ViewModels;

namespace Project.App.Views.Projects;

public partial class ProjectCreateView : ContentPageBase
{
    public ProjectCreateView(ProjectCreateViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
