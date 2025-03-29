﻿using System.Collections.ObjectModel;
using System.Windows.Input;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.Services.Implementations;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

// TODO Add Microsoft DI, Logging
public class TestingViewModel : BaseViewModel
{
    private readonly AppSettings _appSettings;
    private readonly NavigationService _navigationService;
    private readonly Teacher _teacher;
    
    private bool _isChecked = false;
    
    private string _messageText;
    private ObservableCollection<TaskViewModel> _taskViewModels = new ObservableCollection<TaskViewModel>();
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
            return _submitCommand ??= new Command(Submit);
        }
    }
    
    public TestingViewModel(AppSettings appSettings, NavigationService navigationService, Teacher teacher)
    {
        _appSettings = appSettings;
        _navigationService = navigationService;
        _teacher = teacher;
        
        _teacher.GenerateTasksAsync().ContinueWith(task =>
        {
            List<SourceTask> sourceTasks = task.Result.ToList();
            TaskViewModels = new ObservableCollection<TaskViewModel>(sourceTasks.Select(sourceTask=> 
                Map(sourceTask, sourceTasks.IndexOf(sourceTask))));
            
        }, TaskScheduler.FromCurrentSynchronizationContext());

        MessageText = Application.Current.Resources["Tutorial"] as string;
    }

    private TaskViewModel Map(SourceTask sourceTask, int taskIndex)
    {
        return new TaskViewModel(sourceTask, ++taskIndex);
    }
    
    private AnsweredTask Map(TaskViewModel taskViewModel)
    {
        return taskViewModel.GetAnsweredTask();
    }
    
    private async void Submit()
    {
        if (!_isChecked)
        {
            await CheckTasks();
            _isChecked = true;
        }
        else
        {
            await _navigationService.GoBackAsync();
        }
    }

    private async Task CheckTasks()
    {
        IEnumerable<CheckedTask> checkingResult = await _teacher.CheckAnsweredTasksAsync(_taskViewModels.Select(Map));
        
        List<TaskViewModel> taskViewModels = _taskViewModels.ToList();
        List<CheckedTask> checkedTasks = checkingResult.ToList();
        
        int tasksCount = Math.Min(taskViewModels.Count, checkedTasks.Count);

        for (int i = 0; i < tasksCount; i++)
        {
            taskViewModels[i].SetCheckedTask(checkedTasks[i]);
        }

        MessageText = GetCheckingResultText(checkedTasks);
    }

    private string GetCheckingResultText(List<CheckedTask> checkedTasks)
    {
        string resultPattern = Application.Current.Resources["CheckingResult"] as string;

        int correctTasksCount = checkedTasks.Count(task => task.Result);

        return string.Format(resultPattern, correctTasksCount, checkedTasks.Count);
    }

}