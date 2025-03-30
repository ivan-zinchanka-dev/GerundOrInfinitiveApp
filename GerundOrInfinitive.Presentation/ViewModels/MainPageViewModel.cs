using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;
using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private const string VerbsCountTextPattern = "Verbs count: {0}"; 
    
    private readonly AppSettings _appSettings;
    private readonly INavigationService _navigationService;

    private int _verbsCount;
    private string _verbsCountText;

    private Command _startTestingCommand;

    public int VerbsCount
    {
        get => _verbsCount;

        set
        {
            _verbsCount = value;
            OnPropertyChanged();
            VerbsCountText = string.Format(VerbsCountTextPattern, _verbsCount);
        }

    }
    
    public string VerbsCountText
    {
        get => _verbsCountText;

        set
        {
            _verbsCountText = value;
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
    
    public MainPageViewModel(AppSettings appSettings, INavigationService navigationService)
    {
        _appSettings = appSettings;
        _navigationService = navigationService;

        VerbsCount = _appSettings.VerbsCount;
    }

    private async void StartTesting()
    {
        await _navigationService.NavigateToAsync<TestingPage>();
    }
    
}