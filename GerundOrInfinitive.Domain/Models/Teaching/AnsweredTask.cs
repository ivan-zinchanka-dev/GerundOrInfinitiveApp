namespace GerundOrInfinitive.Domain.Models.Teaching;

public struct AnsweredTask
{
    public ExampleTask ExampleTask { get; }
    public string UserAnswer { get; }

    public AnsweredTask(ExampleTask exampleTask, string userAnswer)
    {
        ExampleTask = exampleTask;
        UserAnswer = userAnswer;
    }
}