using System.Windows.Input;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;
using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class MainPageViewModel : BaseViewModel
{
    private readonly INavigationService _navigationService;
    
    private Command _startTestingCommand;
    
    public ICommand StartTestingCommand
    {
        get
        {
            return _startTestingCommand ??= new Command(StartTesting);
        }
    }
    
    public MainPageViewModel(INavigationService navigationService)
    {
        _navigationService = navigationService;
    }

    private async void StartTesting()
    {
        await _navigationService.NavigateToAsync<TestingPage>();
    }

}