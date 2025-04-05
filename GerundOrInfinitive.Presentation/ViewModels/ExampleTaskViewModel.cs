using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class ExampleTaskViewModel : BaseViewModel
{
    private const string CorrectAnswerPattern = "Correct answer: {0}";
    private const string SourceVerbPattern = "{0}) Source verb: <b>{1}</b>";
    
    private string _inputBlackText;
    private string _correctAnswer;
    private CheckingStatus _checkingStatus;
    private bool _isChecked;
    
    private readonly ExampleTask _exampleTask;
    
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

    public ExampleTaskViewModel(ExampleTask exampleTask, int taskNumber)
    {
        _exampleTask = exampleTask;
        (string beforeBlankText, string afterBlankText) = _exampleTask.GetSourceSentenceParts();
        
        BeforeBlankText = beforeBlankText;
        InputBlankText = string.Empty;
        AfterBlankText = afterBlankText;
        CorrectAnswer = string.Empty;
        Status = CheckingStatus.Unchecked;
        IsChecked = false;
        
        SourceVerbText = string.Format(SourceVerbPattern, taskNumber, _exampleTask.UsedWord);
    }

    public void SubmitAnswer()
    {
        _exampleTask.Answer(InputBlankText);
    }

    public void OnTaskChecked()
    {
        CorrectAnswer = string.Format(CorrectAnswerPattern, _exampleTask.CorrectAnswer);
        Status = _exampleTask.CheckingStatus;
        IsChecked = true;
    }
}