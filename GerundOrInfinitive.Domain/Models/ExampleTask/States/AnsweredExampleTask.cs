namespace GerundOrInfinitive.Domain.Models.ExampleTask.States;

public class AnsweredExampleTask : SourceExampleTask
{
    public string UserAnswer { get; }
    
    public AnsweredExampleTask(int exampleId, string sourceSentence, string usedWord, string userAnswer) : 
        base(exampleId, sourceSentence, usedWord)
    {
        UserAnswer = userAnswer;
    }
}