using System.Collections.ObjectModel;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class TestingViewModel : BaseViewModel
{
    private readonly Teacher _teacher;
    private ObservableCollection<TaskViewModel> _taskViewModels = new ObservableCollection<TaskViewModel>();
    private Command _submitCommand;
    
    public ObservableCollection<TaskViewModel> TaskViewModels
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
            Console.WriteLine("SUBMIT");
            
            return _submitCommand ??= new Command(Submit);
        }
    }

    private async void Submit()
    {
        IEnumerable<CheckedTask> checkingResult = await _teacher.CheckAnsweredTasksAsync(_taskViewModels.Select(Map));
        
        List<TaskViewModel> taskViewModels = _taskViewModels.ToList();
        List<CheckedTask> checkedTasks = checkingResult.ToList();
        
        int tasksCount = Math.Min(taskViewModels.Count, checkedTasks.Count);

        for (int i = 0; i < tasksCount; i++)
        {
            taskViewModels[i].SetCheckedTask(checkedTasks[i]);
        }
    }

    public TestingViewModel()
    {
        _teacher = new Teacher(new ExampleRepository(MauiProgram.DatabasePath));
        
        _teacher.GenerateTasksAsync().ContinueWith(task =>
        {
            TaskViewModels = new ObservableCollection<TaskViewModel>(task.Result.Select(Map));
            
        }, TaskScheduler.FromCurrentSynchronizationContext());
    }

    private TaskViewModel Map(SourceTask sourceTask)
    {
        return new TaskViewModel(sourceTask);
    }
    
    private AnsweredTask Map(TaskViewModel taskViewModel)
    {
        return taskViewModel.GetAnsweredTask();
    }
}