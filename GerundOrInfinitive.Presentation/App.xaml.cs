﻿using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation;

public partial class App : Application
{
    private readonly IServiceProvider _serviceProvider;
    private readonly INavigationService _navigationService;
    
    public App(IServiceProvider serviceProvider, INavigationService navigationService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
        
        InitializeComponent();
        
        var mainPage = _serviceProvider.GetService<MainPage>();
        var mainNavigationPage = new NavigationPage(mainPage);
        MainPage = mainNavigationPage;
        _navigationService.Initialize(mainNavigationPage);
    }
}