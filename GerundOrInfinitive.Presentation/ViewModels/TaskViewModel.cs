using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class TaskViewModel : BaseViewModel
{
    private const string CorrectAnswerPattern = "Correct answer: {0}";
    private const string SourceVerbPattern = "{0}) Source verb: <b>{1}</b>";
    
    private string _inputBlackText;
    private string _correctAnswer;
    private CheckingStatus _checkingStatus;
    private bool _isChecked;
    
    private readonly SourceTask _sourceTask;
    private CheckedTask _checkedTask;

    public string SourceVerbText { get; }
    public string BeforeBlankText { get; }
    public string AfterBlankText { get; }

    public string InputBlankText
    {
        get => _inputBlackText;

        set
        {
            _inputBlackText = value;
            OnPropertyChanged();
        }
    }

    public string CorrectAnswer
    {
        get => _correctAnswer;

        set
        {
            _correctAnswer = value;
            OnPropertyChanged();
        }
    }
    
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

    public TaskViewModel(SourceTask sourceTask, int taskNumber)
    {
        _sourceTask = sourceTask;

        string[] substrings = _sourceTask.SourceSentence.Split("...");
        BeforeBlankText = substrings[0];
        InputBlankText = string.Empty;
        AfterBlankText = substrings[1];
        CorrectAnswer = string.Empty;
        Status = CheckingStatus.Unchecked;
        IsChecked = false;
        
        SourceVerbText = string.Format(SourceVerbPattern, taskNumber, _sourceTask.UsedWord);
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