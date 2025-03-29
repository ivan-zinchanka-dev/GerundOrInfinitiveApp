using GerundOrInfinitive.Presentation.Services;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels;
using GerundOrInfinitive.Presentation.Views;
using Microsoft.Extensions.Logging;

namespace GerundOrInfinitive.Presentation;

public static class MauiProgram
{
    public static string DatabasePath { get; private set; } = null;

    public static MauiApp CreateMauiApp()
    {
        Task.Run(async () =>
        {
            DatabasePath = await MauiAssetDeployer.DeployAssetIfNeed("gerund_or_infinitive.db");
        });
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        builder.Services
            .AddSingleton<NavigationService>()
            .AddTransient<MainPageViewModel>()
            .AddTransient<MainPage>()
            .AddTransient<TestingViewModel>()
            .AddTransient<TestingPage>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}