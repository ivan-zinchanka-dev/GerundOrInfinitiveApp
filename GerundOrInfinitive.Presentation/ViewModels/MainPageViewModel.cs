using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Settings;
using GerundOrInfinitive.Presentation.ViewModels.Base;
using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation.ViewModels;

internal class MainPageViewModel : BaseViewModel
{
    private const string ExamplesCountTextPattern = "Count of examples: {0}"; 
    
    private readonly IAppSettings _appSettings;
    private readonly INavigationService _navigationService;
    
    private string _examplesCountText;
    private Command _startTestingCommand;

    public int MinExamplesCount => _appSettings.MinExamplesCount;
    public int MaxExamplesCount => _appSettings.MaxExamplesCount;
    
    public int ExamplesCount
    {
        get => _appSettings.ExamplesCount;

        set
        {
            _appSettings.ExamplesCount = value;
            OnPropertyChanged();
            ExamplesCountText = string.Format(ExamplesCountTextPattern, _appSettings.ExamplesCount);
        }
    }
    
    public string ExamplesCountText
    {
        get => _examplesCountText;

        set
        {
            _examplesCountText = value;
            OnPropertyChanged();
        }
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
        
        ExamplesCountText = string.Format(ExamplesCountTextPattern, _appSettings.ExamplesCount);
    }

    private async void StartTesting()
    {
        await _navigationService.NavigateToAsync<TestingPage>();
    }
    
}