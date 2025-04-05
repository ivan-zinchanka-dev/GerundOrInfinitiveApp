namespace GerundOrInfinitive.Domain.Models.ExampleTask.States;

public class SourceExampleTask : ExampleTaskState
{
    public SourceExampleTask(int exampleId, string sourceSentence, string usedWord) : base(exampleId, sourceSentence, usedWord)
    {
    }
}