namespace GerundOrInfinitive.Domain.Models.Settings;

public interface IAppSettings
{
    public int ExamplesCount { get; set; }
    public int MinExamplesCount { get; }
    public int MaxExamplesCount { get; }
    public bool ShowAlertDialog { get; set; }
    public string DatabasePath { get; }
}