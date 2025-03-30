using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table("AlternativeCorrectAnswers")]
public class AlternativeCorrectAnswer
{
    [PrimaryKey, AutoIncrement, Column("Id")]
    public int Id { get; set; }
    
    //TODO Shared primary Key  ?  Alter answer in result View
    [Column("ExampleId")]
    public int ExampleId { get; set; }
    
    [Column("Answer")]
    public string Answer { get; set; }
}