using System.Collections.ObjectModel;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

internal class TestingViewModel : BaseViewModel
{
    private readonly IAppSettings _appSettings;
    private readonly AppResources _appResources;
    private readonly INavigationService _navigationService;
    private readonly Teacher _teacher;

    private bool _isChecked = false;
    
    private string _messageText;
    private ObservableCollection<ExampleTaskViewModel> _taskViewModels = new ObservableCollection<ExampleTaskViewModel>();
    private Command _submitCommand;
    
    public string MessageText
    {
        get => _messageText;

        set
        {
            _messageText = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<ExampleTaskViewModel> TaskViewModels
    {
        get => _taskViewModels;

        set
        {
            _taskViewModels = value;
            OnPropertyChanged();
        }
    }

    public ICommand SubmitCommand
    {
        get
        {
            return _submitCommand ??= new Command(Submit);
        }
    }
    
    public TestingViewModel(
        IAppSettings appSettings, 
        AppResources appResources, 
        INavigationService navigationService, 
        Teacher teacher)
    {
        _appSettings = appSettings;
        _appResources = appResources;
        _navigationService = navigationService;
        _teacher = teacher;
        
        _teacher.NewTasksAsync().ContinueWith(task =>
        {
            List<ExampleTask> sourceTasks = task.Result.ToList();
            var taskViewModels = new List<ExampleTaskViewModel>(sourceTasks.Capacity);
            
            for (int i = 0; i < sourceTasks.Count; i++)
            {
                taskViewModels.Add(Map(sourceTasks[i], i));
            }
            
            TaskViewModels = new ObservableCollection<ExampleTaskViewModel>(taskViewModels);
            
        }, TaskScheduler.FromCurrentSynchronizationContext());

        MessageText = _appResources.TutorialString;
    }
    
    private ExampleTaskViewModel Map(ExampleTask exampleTask, int taskIndex)
    {
        return new ExampleTaskViewModel(exampleTask, ++taskIndex);
    }
    
    private async void Submit()
    {
        if (!_isChecked)
        { 
            await CheckTasksAsync();
            _isChecked = true;
        }
        else
        {
            await _navigationService.GoBackAsync();
        }
    }

    private async Task CheckTasksAsync()
    {
        foreach (ExampleTaskViewModel viewModel in _taskViewModels)
        {
            viewModel.SubmitAnswer();
        }
        
        await _teacher.CheckTasksAsync();
        
        foreach (ExampleTaskViewModel viewModel in _taskViewModels)
        {
            viewModel.OnTaskChecked();
        }
        
        MessageText = GetCheckingResultText();
    }

    private string GetCheckingResultText()
    {
        int correctTasksCount = _taskViewModels.Count(viewModel => viewModel.Status == CheckingStatus.Correct);
        return string.Format(_appResources.CheckingResultString, correctTasksCount, _taskViewModels.Count);
    }
}