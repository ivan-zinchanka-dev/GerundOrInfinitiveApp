using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;
using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;
    private readonly INavigationService _navigationService;
    
    private string _validationErrorMessage;
    private Command _startTestingCommand;

    public string ValidationErrorMessage
    {
        get => _validationErrorMessage;

        set
        {
            _validationErrorMessage = value;
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
    }

    private async void StartTesting()
    {
        await _navigationService.NavigateToAsync<TestingPage>();
    }

}