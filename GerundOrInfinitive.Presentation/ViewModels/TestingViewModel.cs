using System.Collections.ObjectModel;
using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class TestingViewModel : BaseViewModel
{
    private readonly Teacher _teacher;
    private ObservableCollection<SourceTaskViewModel> _sourceTasks = new ObservableCollection<SourceTaskViewModel>();
    
    public ObservableCollection<SourceTaskViewModel> SourceTasks
    {
        get => _sourceTasks;

        set
        {
            _sourceTasks = value;
            OnPropertyChanged();
        }
    }

    public TestingViewModel()
    {
        _teacher = new Teacher();
        SourceTasks = new ObservableCollection<SourceTaskViewModel>(_teacher.GenerateTasks().Select(Map));
    }

    private SourceTaskViewModel Map(SourceTask sourceTask)
    {
        return new SourceTaskViewModel(sourceTask);
    }
}