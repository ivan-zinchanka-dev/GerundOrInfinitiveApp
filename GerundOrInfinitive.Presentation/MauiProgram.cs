﻿using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels;
using GerundOrInfinitive.Presentation.Views;
using Microsoft.Extensions.Logging;

namespace GerundOrInfinitive.Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    { 
        string databasePath = null;
        
        Task deployTask = Task.Run(async () =>
        {
            databasePath = await MauiAssetDeployer.DeployAssetIfNeed("gerund_or_infinitive.db");
        });
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });
        
        deployTask.GetAwaiter().GetResult();
        
        builder.Services
            .AddSingleton<AppSettings>(new AppSettings(databasePath))
            .AddSingleton<ExampleRepository>()
            .AddTransient<Teacher>()
            .AddSingleton<NavigationService>()
            .AddTransient<MainPageViewModel>()
            .AddTransient<MainPage>()
            .AddTransient<TestingViewModel>()
            .AddTransient<TestingPage>();
        
#if DEBUG
        builder.Logging.AddDebug();
#endif
      
        builder.Logging.AddConsole();
        
        
        
        return builder.Build();
    }
}