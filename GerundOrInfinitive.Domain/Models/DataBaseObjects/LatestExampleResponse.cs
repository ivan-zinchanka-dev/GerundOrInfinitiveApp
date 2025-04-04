using SQLite;

namespace GerundOrInfinitive.Domain.Models.DataBaseObjects;

[Table($"{nameof(LatestExampleResponse)}s")]
public class LatestExampleResponse
{
    [PrimaryKey, Column(nameof(ExampleId))]
    public int ExampleId { get; set; }

    [Column(nameof(Result))]
    public bool Result { get; set; }
    
    [Column(nameof(Time))]
    public long TimeUnixMilliseconds { get; set; }

    [Ignore]
    public DateTime Time
    {
        get => DateTimeOffset.FromUnixTimeMilliseconds(TimeUnixMilliseconds).UtcDateTime;
        set => TimeUnixMilliseconds = new DateTimeOffset(value).ToUnixTimeMilliseconds();
    }

    public void SetCurrentTime()
    {
        Time = DateTime.UtcNow;
    }

    // TODO Сначало неверные, затем верные, которых давно не было
}