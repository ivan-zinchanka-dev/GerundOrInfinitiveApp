using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table($"{nameof(AlternativeCorrectAnswer)}s")]
public class AlternativeCorrectAnswer
{
    [PrimaryKey, AutoIncrement, Column(nameof(Id))]
    public int Id { get; set; }
    
    [Column(nameof(ExampleId))]
    public int ExampleId { get; set; }
    
    [Column(nameof(Answer))]
    public string Answer { get; set; }
}