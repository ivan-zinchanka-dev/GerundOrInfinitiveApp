using System.Collections.ObjectModel;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;
using ReactiveUI;

namespace GerundOrInfinitive.Presentation.ViewModels;

internal class TestingViewModel : ReactiveObject
{
    private readonly IAppSettings _appSettings;
    private readonly AppResources _appResources;
    private readonly INavigationService _navigationService;
    private readonly Teacher _teacher;
    
    private string _messageText;
    private ObservableCollection<ExampleTaskViewModel> _taskViewModels = new ObservableCollection<ExampleTaskViewModel>();
    private Command _submitCommand;
    private Command _gotItCommand;
    
    public event Func<Task<bool>> OnPreSubmit;
    public event Action<bool> OnPostSubmit;
    
    public string MessageText
    {
        get => _messageText;
        set => this.RaiseAndSetIfChanged(ref _messageText, value);
    }

    public ObservableCollection<ExampleTaskViewModel> TaskViewModels
    {
        get => _taskViewModels;
        set => this.RaiseAndSetIfChanged(ref _taskViewModels, value);
    }

    public ICommand SubmitCommand
    {
        get
        {
            return _submitCommand ??= new Command(Submit);
        }
    }
    
    public ICommand GotItCommand
    {
        get
        {
            return _gotItCommand ??= new Command(GotIt);
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
        bool accepted = false;
            
        if (OnPreSubmit != null)
        {
            accepted = await OnPreSubmit();
        }

        if (accepted)
        {
            await CheckTasksAsync();
        }
        
        OnPostSubmit?.Invoke(accepted);
    }

    private async void GotIt()
    {
        await _navigationService.GoBackAsync();
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