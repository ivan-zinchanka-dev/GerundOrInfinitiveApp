using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GerundOrInfinitive.Domain.Models.Settings;

namespace GerundOrInfinitive.Presentation.Settings;

internal class AppSettings : IAppSettings, INotifyPropertyChanged
{
    private const string ExamplesCountKey = "examples_count";
    
    private const int MinExamplesCount = 5;
    private const int DefaultExamplesCount = 10;
    private const int MaxExamplesCount = 20;
    
    [Range(MinExamplesCount, MaxExamplesCount)]
    public int ExamplesCount
    {
        get => Preferences.Get(ExamplesCountKey, DefaultExamplesCount);
        set
        {
            Preferences.Set(ExamplesCountKey, value);
            OnPropertyChanged();
        }
    }
    
    public string DatabasePath { get; }

    public AppSettings(string databasePath)
    {
        DatabasePath = databasePath;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}