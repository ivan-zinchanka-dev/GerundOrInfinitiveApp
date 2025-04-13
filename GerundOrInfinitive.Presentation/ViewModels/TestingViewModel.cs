using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Subjects;
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
    private readonly Subject<bool> _canUseCommands = new Subject<bool>();
    
    private string _messageText;
    private ObservableCollection<ExampleTaskViewModel> _taskViewModels = new();
    
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
        
        _canUseCommands.OnNext(false);
        _teacher.NewTasksAsync().ContinueWith(task =>
        {
            ShowTasks(task.Result.ToList());
            _canUseCommands.OnNext(true);
            
        }, TaskScheduler.FromCurrentSynchronizationContext());

        MessageText = _appResources.TutorialString;
        
        SubmitCommand = ReactiveCommand.CreateFromTask(Submit, _canUseCommands);
        GotItCommand = ReactiveCommand.CreateFromTask(GotIt, _canUseCommands);
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