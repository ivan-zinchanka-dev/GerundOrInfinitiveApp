using GerundOrInfinitive.Domain.Models.Settings;

namespace GerundOrInfinitive.Tests.Settings;

public class MockAppSettings : IAppSettings
{
    private const string ResourcesFolder = "Resources";
    private const string DatabaseFileName = "gerund_or_infinitive.db";
    
    public int ExamplesCount { get; set; } = 10;
    public int MinExamplesCount => 5;
    public int MaxExamplesCount => 15;
    public bool ShowAlertDialog { get; set; } = false;
    public string DatabasePath => Path.Combine(Directory.GetCurrentDirectory(), ResourcesFolder, DatabaseFileName);
}