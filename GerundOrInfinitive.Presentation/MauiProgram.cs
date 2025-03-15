﻿using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services;
using Microsoft.Extensions.Logging;

namespace GerundOrInfinitive.Presentation;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        Task.Run(async () =>
        {
            await ResourcePreloader.CopyDatabaseIfNotExists();
        });
        
        Console.WriteLine("Copied");
        
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif
        
        return builder.Build();
    }
}