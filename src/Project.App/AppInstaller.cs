using Project.App.Services;

namespace Project.App;

public static class AppInstaller
{
    public static IServiceCollection AddAppServices(this IServiceCollection services)
    {
        services.AddSingleton<AppShell>();
        services.AddTransient<INavigationService, NavigationService>();
        return services;
    }
}
