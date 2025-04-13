using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Views;

internal partial class TestingPage : ContentPage
{
    private readonly TestingViewModel _viewModel;
    
    public TestingPage(TestingViewModel viewModel)
    {
        _viewModel = viewModel;
        BindingContext = _viewModel;
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        _viewModel.OnPreSubmit += DisplayAlert;
    }

    private Task<bool> DisplayAlert()
    {
        return DisplayAlert("Confirm the action", "Are you sure you want to start checking?", "Yes", "No");
    }
    
    protected override void OnDisappearing()
    {
        _viewModel.OnPreSubmit -= DisplayAlert;
    }
}