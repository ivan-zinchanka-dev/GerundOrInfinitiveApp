namespace GerundOrInfinitive.Domain.Models.Teaching;

public struct AnsweredTask
{
    public SourceTask SourceTask { get; }
    public string UserAnswer { get; }

    public AnsweredTask(SourceTask sourceTask, string userAnswer)
    {
        SourceTask = sourceTask;
        UserAnswer = userAnswer;
    }
}