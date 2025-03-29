namespace GerundOrInfinitive.Domain.Models.Settings;

public class AppSettings
{
    public string DatabasePath { get; private set; } = null;

    public AppSettings(string databasePath)
    {
        DatabasePath = databasePath;
    }
}