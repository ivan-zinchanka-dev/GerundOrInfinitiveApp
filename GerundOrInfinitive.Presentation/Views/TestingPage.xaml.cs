﻿using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Views;

public partial class TestingPage : ContentPage
{
    private readonly TestingViewModel _viewModel;
    
    public TestingPage(TestingViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }
}