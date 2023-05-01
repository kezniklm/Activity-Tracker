using Project.App.ViewModels;

namespace Project.App.Views.Login;

public partial class LoginView : ContentPageBase
{
    public LoginView(LoginViewModel viewModel) : base(viewModel) => InitializeComponent();
}
