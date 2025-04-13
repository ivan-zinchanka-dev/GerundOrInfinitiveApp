using System.Reactive.Linq;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Views;
using ReactiveUI;

namespace GerundOrInfinitive.Presentation.ViewModels;

internal class MainPageViewModel : ReactiveObject
{
    private const string ExamplesCountTextPattern = "Count of examples: {0}"; 
    
    private readonly IAppSettings _appSettings;
    private readonly INavigationService _navigationService;
    
    private readonly ObservableAsPropertyHelper<string> _examplesCountText;
    private Command _startTestingCommand;

    public int MinExamplesCount => _appSettings.MinExamplesCount;
    public int MaxExamplesCount => _appSettings.MaxExamplesCount;
    
    public int ExamplesCount
    {
        get => _appSettings.ExamplesCount;
        set => _appSettings.ExamplesCount = value;
    }
    
    public string ExamplesCountText => _examplesCountText.Value;

    public bool ShowAlertDialog
    {
        get => _appSettings.ShowAlertDialog;
        set => _appSettings.ShowAlertDialog = value;
    }

    public ICommand StartTestingCommand
    {
        get
        {
            return _startTestingCommand ??= new Command(StartTesting);
        }
    }
    
    public MainPageViewModel(IAppSettings appSettings, INavigationService navigationService)
    {
        _appSettings = appSettings;
        _navigationService = navigationService;

        _appSettings
            .WhenAnyValue(settings => settings.ExamplesCount)
            .Select(examplesCount => string.Format(ExamplesCountTextPattern, examplesCount))
            .ToProperty(this, viewModel => viewModel.ExamplesCountText, out _examplesCountText);
    }

    private async void StartTesting()
    {
        await _navigationService.NavigateToAsync<TestingPage>();
    }
}