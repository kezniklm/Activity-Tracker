namespace Project.App;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<AppShell>();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var view = base.CreateWindow(activationState);
        view.Width = 1100;
        view.Height = 600;
        view.MinimumWidth = 1100;
        return view;
    }
}
