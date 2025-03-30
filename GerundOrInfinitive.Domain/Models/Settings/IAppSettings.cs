namespace GerundOrInfinitive.Domain.Models.Settings;

public interface IAppSettings
{
    public int ExamplesCount { get; set; }
    public int MinExamplesCount { get; }
    public int MaxExamplesCount { get; }
    public string DatabasePath { get; }
}