using Project.App.ViewModels;

namespace Project.App.Views.Login;

public partial class UserDetailView : ContentPageBase
{
    public UserDetailView(UserDetailViewModel viewModel) : base(viewModel) => InitializeComponent();
}
