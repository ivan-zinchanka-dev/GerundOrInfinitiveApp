namespace GerundOrInfinitive.Domain.Models.ExampleTask.States;

public abstract class ExampleTaskState
{
    public int ExampleId { get; }
    public string SourceSentence { get; }
    public string UsedWord { get; }

    protected ExampleTaskState(int exampleId, string sourceSentence, string usedWord)
    {
        ExampleId = exampleId;
        SourceSentence = sourceSentence;
        UsedWord = usedWord;
    }
}