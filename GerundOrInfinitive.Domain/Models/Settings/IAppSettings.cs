namespace GerundOrInfinitive.Domain.Models.Settings;

public interface IAppSettings
{
    public int ExamplesCount { get; set; }
    public string DatabasePath { get; }
}