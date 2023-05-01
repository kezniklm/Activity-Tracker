using Project.App.ViewModels;

namespace Project.App.Views.Login;

public partial class UserEditView : ContentPageBase
{
    public UserEditView(UserEditViewModel viewModel) : base(viewModel) => InitializeComponent();
}
