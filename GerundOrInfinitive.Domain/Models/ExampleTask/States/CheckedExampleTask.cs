namespace GerundOrInfinitive.Domain.Models.ExampleTask.States;

public class CheckedExampleTask : AnsweredExampleTask
{
    public string CorrectAnswer { get; }
    public string AlternativeCorrectAnswer { get; }
    
    public bool Result => GetResult();
    
    public CheckedExampleTask(int exampleId, string sourceSentence, string usedWord, 
        string userAnswer, string correctAnswer, string alternativeCorrectAnswer = null) : 
        base(exampleId, sourceSentence, usedWord, userAnswer)
    {
        CorrectAnswer = correctAnswer;
        AlternativeCorrectAnswer = alternativeCorrectAnswer;
    }
    
    private bool GetResult()
    {
        return AreEqualAnswers(UserAnswer, CorrectAnswer) || 
               (AlternativeCorrectAnswer != null && AreEqualAnswers(UserAnswer, AlternativeCorrectAnswer));
    }

    private static bool AreEqualAnswers(string answerOne, string answerTwo)
    {
        answerOne = answerOne?.Trim();
        answerTwo = answerTwo?.Trim();

        return string.Equals(answerOne, answerTwo, StringComparison.OrdinalIgnoreCase);
    }
}