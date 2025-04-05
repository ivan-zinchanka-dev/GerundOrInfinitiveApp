using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.ExampleTask.States;

namespace GerundOrInfinitive.Domain.Models.ExampleTask;

public class ExampleTask
{
    private const string Gap = "...";
    
    private readonly Example _example;
    private SourceExampleTask _state;

    internal int ExampleId => _example.Id;
    public string SourceSentence => _example.SourceSentence;
    public string UsedWord => _example.UsedWord;
    public string CorrectAnswer => _example.CorrectAnswer;

    public CheckingStatus CheckingStatus { get; private set; } = CheckingStatus.Unchecked;
    
    public ValueTuple<string, string> GetSourceSentenceParts()
    {
        string[] parts = SourceSentence.Split(Gap);
        return new ValueTuple<string, string>(parts[0], parts[1]);
    }

    internal ExampleTask(Example example)
    {
        _example = example;
        _state = new SourceExampleTask(
            _example.Id, 
            _example.SourceSentence, 
            _example.UsedWord);
    }

    public void Answer(string userAnswer)
    {
        _state = new AnsweredExampleTask(
            _example.Id, 
            _example.SourceSentence, 
            _example.UsedWord, 
            userAnswer);
    }
    
    internal bool Check()
    {
        string userAnswer = string.Empty;
        
        if (_state is AnsweredExampleTask answeredTask)
        {
            userAnswer = answeredTask.UserAnswer;
        }

        var checkedTask = new CheckedExampleTask(
            _example.Id, 
            _example.SourceSentence, 
            _example.UsedWord, 
            userAnswer,
            _example.CorrectAnswer, 
            _example.AlternativeCorrectAnswer?.Answer);
        
        _state = checkedTask;
        CheckingStatus = checkedTask.Result ? CheckingStatus.Correct : CheckingStatus.Incorrect;

        return CheckingStatus == CheckingStatus.Correct;
    }
    
}