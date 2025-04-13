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
        _viewModel.OnPostSubmit += UpdateActionButtonIfNeed;
    }

    private Task<bool> DisplayAlert()
    {
        return DisplayAlert("Confirm the action", "Are you sure you want to start checking?", "Yes", "No");
    }

    private void UpdateActionButtonIfNeed(bool need)
    {
        if (need)
        {
            _actionButton.Command = _viewModel.GotItCommand;
            _actionButton.Text = "Got it";
        }
    }

    protected override void OnDisappearing()
    {
        _viewModel.OnPostSubmit -= UpdateActionButtonIfNeed;
        _viewModel.OnPreSubmit -= DisplayAlert;
    }
}