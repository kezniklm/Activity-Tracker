using System.Reflection;
using System.Resources;
using CommunityToolkit.Maui;
using Microsoft.Extensions.Configuration;
using Project.App.Models;
using Project.App.Services;
using Project.BL;

[assembly: NeutralResourcesLanguage("en")]

namespace Project.App;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        MauiAppBuilder builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        ConfigureAppSettings(builder);
        builder.Services
            .AddDALServices(builder.Configuration)
            .AddBLServices()
            .AddAppServices();

        MauiApp app = builder.Build();
        app.Services.GetRequiredService<IDbMigrator>().Migrate();
        RegisterRouting(app.Services.GetRequiredService<INavigationService>());

        return app;
    }

    private static void ConfigureAppSettings(MauiAppBuilder builder)
    {
        ConfigurationBuilder configurationBuilder = new();

        Assembly assembly = Assembly.GetExecutingAssembly();
        const string appSettingsFilePath = "Project.App.appsettings.json";
        using Stream? appSettingsStream = assembly.GetManifestResourceStream(appSettingsFilePath);
        if (appSettingsStream is not null)
        {
            configurationBuilder.AddJsonStream(appSettingsStream);
        }

        IConfigurationRoot configuration = configurationBuilder.Build();
        builder.Configuration.AddConfiguration(configuration);
    }

    private static void RegisterRouting(INavigationService navigationService)
    {
        foreach (RouteModel route in navigationService.Routes)
        {
            Routing.RegisterRoute(route.Route, route.ViewType);
        }
    }
}
