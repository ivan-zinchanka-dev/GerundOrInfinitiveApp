using System.Collections.ObjectModel;
using System.Reactive;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services.Contracts;
using GerundOrInfinitive.Presentation.Services.Implementations;
using ReactiveUI;

namespace GerundOrInfinitive.Presentation.ViewModels;

internal class TestingViewModel : ReactiveObject
{
    private readonly AppResources _appResources;
    private readonly INavigationService _navigationService;
    private readonly Teacher _teacher;
    
    private string _messageText;
    private ObservableCollection<ExampleTaskViewModel> _taskViewModels = new();
    private Task _showExamplesRoutine;
    
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

    public ReactiveCommand<Unit, Unit> SubmitCommand { get; }
    public ReactiveCommand<Unit, Unit> GotItCommand { get; }
    
    public TestingViewModel(
        AppResources appResources, 
        INavigationService navigationService, 
        Teacher teacher)
    {
        _appResources = appResources;
        _navigationService = navigationService;
        _teacher = teacher;
        
        _showExamplesRoutine = _teacher.NewTasksAsync().ContinueWith(task =>
        {
            ShowTasks(task.Result.ToList());

        }, TaskScheduler.FromCurrentSynchronizationContext());

        MessageText = _appResources.TutorialString;

        //IObservable<bool> canUseCommands = CanUseCommands();
        SubmitCommand = ReactiveCommand.CreateFromTask(Submit/*, canUseCommands*/);
        GotItCommand = ReactiveCommand.CreateFromTask(GotIt/*, canUseCommands*/);
    }

    private void ShowTasks(List<ExampleTask> sourceTasks)
    {
        var taskViewModels = new List<ExampleTaskViewModel>(sourceTasks.Capacity);
            
        for (int i = 0; i < sourceTasks.Count; i++)
        {
            taskViewModels.Add(Map(sourceTasks[i], i));
        }
            
        TaskViewModels = new ObservableCollection<ExampleTaskViewModel>(taskViewModels);
    }

    private ExampleTaskViewModel Map(ExampleTask exampleTask, int taskIndex)
    {
        return new ExampleTaskViewModel(exampleTask, ++taskIndex);
    }

    private IObservable<bool> CanUseCommands()
    {
        return this.WhenAnyValue(viewModel => 
            viewModel._showExamplesRoutine != null && viewModel._showExamplesRoutine.IsCompleted);
    }

    private async Task Submit()
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

    private async Task GotIt()
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