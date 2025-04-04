using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table($"{nameof(LatestExampleResponse)}s")]
public class LatestExampleResponse
{
    [PrimaryKey, Column(nameof(ExampleId))]
    public int ExampleId { get; set; }

    [Column(nameof(Result))]
    public bool Result { get; set; }
    
    // TODO Reflect Unix in Table
    [Column(nameof(Time))]
    public long TimeUnixMilliseconds { get; set; }

    [Ignore]
    public DateTime Time
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(TimeUnixMilliseconds).UtcDateTime;
        set => TimeUnixMilliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
    }
    
    // TODO Сначало неверные, затем верные, которых давно не было
}