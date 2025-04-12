using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class ExampleTaskViewModel : BaseViewModel
{
    private const string SourceVerbPattern = "{0}) Source verb: <b>{1}</b>. ";
    private const string CorrectAnswerPattern = "Correct answer: {0}";
    
    private string _headerText;
    private string _inputBlackText;
    private CheckingStatus _checkingStatus;
    private bool _isChecked;
    
    private readonly ExampleTask _exampleTask;
    
    public string HeaderText
    {
        get => _headerText;

        set
        {
            _headerText = value;
            OnPropertyChanged();
        }
    }
    
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
        Status = CheckingStatus.Unchecked;
        IsChecked = false;
        
        HeaderText = string.Format(SourceVerbPattern, taskNumber, _exampleTask.UsedWord);
    }

    public void SubmitAnswer()
    {
        _exampleTask.Answer(InputBlankText);
    }

    public void OnTaskChecked()
    {
        HeaderText += string.Format(CorrectAnswerPattern, _exampleTask.CorrectAnswer);
        Status = _exampleTask.CheckingStatus;
        IsChecked = true;
    }
}