namespace GerundOrInfinitive.Domain.Models.Teaching;

public readonly struct CheckedTask
{
    public SourceTask SourceTask { get; }
    public string UserAnswer { get; }
    public string CorrectAnswer { get; }
    public string AlternativeCorrectAnswer { get; }
    
    private const string Gap = "...";
    public bool Result => GetResult();
    public string CorrectSentence => GetCorrectSentence();

    public CheckedTask(
        SourceTask sourceTask, 
        string userAnswer, 
        string correctAnswer, 
        string alternativeCorrectAnswer)
    {
        SourceTask = sourceTask;
        UserAnswer = userAnswer;
        CorrectAnswer = correctAnswer;
        AlternativeCorrectAnswer = alternativeCorrectAnswer;
    }

    public static CheckedTask Invalid => 
        new CheckedTask(
            new SourceTask(-1, string.Empty, string.Empty), 
            string.Empty, string.Empty, string.Empty);

    private string GetCorrectSentence()
    {
        return SourceTask.SourceSentence.Replace(Gap, CorrectAnswer);
    }
    
    // TODO User answer is null
    private bool GetResult()
    {
        return AreEqualAnswers(UserAnswer, CorrectAnswer) || AreEqualAnswers(UserAnswer, AlternativeCorrectAnswer);
    }

    private static bool AreEqualAnswers(string answerOne, string answerTwo)
    {
        return string.Equals(answerOne.Trim(), answerTwo.Trim(), StringComparison.OrdinalIgnoreCase);
    }
}