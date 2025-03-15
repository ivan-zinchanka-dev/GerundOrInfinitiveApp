using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table("Examples")]
public class Example
{
    [PrimaryKey, AutoIncrement, Column("Id")]
    public int Id { get; set; }
    
    [Column("SourceSentence")]
    public string SourceSentence { get; set; }
    
    [Column("UsedWord")]
    public string UsedWord { get; set; }
    
    [Column("CorrectAnswer")]
    public string CorrectAnswer { get; set; }
}