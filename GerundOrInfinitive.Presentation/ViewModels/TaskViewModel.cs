using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class TaskViewModel : BaseViewModel
{
    private const string CorrectAnswerPattern = "Correct answer: {0}";
    
    private readonly SourceTask _sourceTask;
    private CheckedTask _checkedTask;
    private CheckingStatus _checkingStatus;
    private bool _isChecked;
    
    public string BeforeBlankText { get; }
    public string InputBlankText { get; }
    public string AfterBlankText { get; }

    public string CorrectAnswer { get; private set; }

    public CheckingStatus Status
    {
        get => _checkingStatus;

        set
        {
            _checkingStatus = value;
            OnPropertyChanged();
        }
    }

    public bool IsChecked
    {
        get => _isChecked;

        set
        {
            _isChecked = value;
            OnPropertyChanged();
        }
    }

    public TaskViewModel(SourceTask sourceTask)
    {
        _sourceTask = sourceTask;

        string[] substrings = _sourceTask.SourceSentence.Split("...");
        BeforeBlankText = substrings[0];
        InputBlankText = string.Empty;
        AfterBlankText = substrings[1];
        CorrectAnswer = string.Empty;
        Status = CheckingStatus.Unchecked;
        IsChecked = false;
    }

    public AnsweredTask GetAnsweredTask()
    {
        return new AnsweredTask(_sourceTask, InputBlankText);
    }

    public void SetCheckedTask(CheckedTask checkedTask)
    {
        _checkedTask = checkedTask;
        CorrectAnswer = string.Format(CorrectAnswerPattern, _checkedTask.CorrectAnswer);
        Status = _checkedTask.Result ? CheckingStatus.Correct : CheckingStatus.Incorrect;
        IsChecked = true;
    }
}