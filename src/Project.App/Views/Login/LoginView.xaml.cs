using Project.App.ViewModels;

namespace Project.App.Views.Login;

public partial class LoginView
{
    public LoginView(LoginViewModel viewModel) : base(viewModel)
    {
        InitializeComponent();
    }
}
