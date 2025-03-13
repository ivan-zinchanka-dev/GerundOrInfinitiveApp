namespace GerundOrInfinitive.Domain.Models.Teaching;

public readonly struct ExampleTask
{
    public int TaskId { get; }
    public string SourceSentence { get; }
    public string UsedWord { get; }
    
    public ExampleTask(int taskId, string sourceSentence, string usedWord)
    {
        TaskId = taskId;
        SourceSentence = sourceSentence;
        UsedWord = usedWord;
    }
}