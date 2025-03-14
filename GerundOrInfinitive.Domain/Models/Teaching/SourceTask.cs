namespace GerundOrInfinitive.Domain.Models.Teaching;

public readonly struct SourceTask
{
    public int TaskId { get; }
    public string SourceSentence { get; }
    public string UsedWord { get; }
    
    public SourceTask(int taskId, string sourceSentence, string usedWord)
    {
        TaskId = taskId;
        SourceSentence = sourceSentence;
        UsedWord = usedWord;
    }
}