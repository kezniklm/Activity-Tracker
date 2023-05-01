using Project.App.ViewModels;

namespace Project.App.Views.Projects;

public partial class ProjectEditView : ContentPageBase
{
    public ProjectEditView(ProjectEditViewModel viewModel) : base(viewModel) => InitializeComponent();
}
