using GerundOrInfinitive.Domain.Models.ExampleTask;
using ReactiveUI;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class ExampleTaskViewModel : ReactiveObject
{
    private const string SourceVerbPattern = "{0}) Source verb: <b>{1}</b>. ";
    private const string CorrectAnswerPattern = "Correct form: <b>{0}</b>";
    
    private string _headerText;
    private string _inputBlackText;
    private CheckingStatus _checkingStatus;
    private bool _isChecked;
    
    private readonly ExampleTask _exampleTask;
    
    public string HeaderText
    {
        get => _headerText;
        set => this.RaiseAndSetIfChanged(ref _headerText, value);
    }
    
    public string BeforeBlankText { get; }
    public string AfterBlankText { get; }

    public string InputBlankText
    {
        get => _inputBlackText;
        set => this.RaiseAndSetIfChanged(ref _inputBlackText, value);
    }
    
    public CheckingStatus Status
    {
        get => _checkingStatus;
        set => this.RaiseAndSetIfChanged(ref _checkingStatus, value);
    }

    public bool IsChecked
    {
        get => _isChecked;
        set => this.RaiseAndSetIfChanged(ref _isChecked, value);
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