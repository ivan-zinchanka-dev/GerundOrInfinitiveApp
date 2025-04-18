﻿using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Views;

internal partial class MainPage : ContentPage
{
    private MainPageViewModel _viewModel;
    
    public MainPage(MainPageViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }
}