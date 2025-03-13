namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

public class Answer
{
    public Guid Id { get; private set; }
    public int ExampleId { get; private set; }
    public DateTime ReceivingTime { get; private set; }
    public bool Result { get; private set; }
}