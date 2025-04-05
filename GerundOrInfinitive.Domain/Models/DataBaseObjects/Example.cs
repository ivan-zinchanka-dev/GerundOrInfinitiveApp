using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table($"{nameof(Example)}s")]
internal class Example
{
    [PrimaryKey, AutoIncrement, Column(nameof(Id))]
    public int Id { get; set; }
    
    [Column(nameof(SourceSentence))]
    public string SourceSentence { get; set; }
    
    [Column(nameof(UsedWord))]
    public string UsedWord { get; set; }
    
    [Column(nameof(CorrectAnswer))]
    public string CorrectAnswer { get; set; }
    
    [Ignore]
    public AlternativeCorrectAnswer AlternativeCorrectAnswer { get; set; }
}