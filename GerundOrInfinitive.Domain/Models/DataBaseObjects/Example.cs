namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

public class Example
{
    private const string Gap = "...";
    public int Id { get; set; }
    public string SourceSentence { get; set; }
    public string UsedWord { get; set; }
    public string CorrectAnswer { get; set; }
    public string AlternativeCorrectAnswer { get; set; }
    
    public string GetCorrectSentence() => SourceSentence.Replace(Gap, CorrectAnswer);
}