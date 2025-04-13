using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using GerundOrInfinitive.Domain.Models.Settings;

namespace GerundOrInfinitive.Presentation.Settings;

internal class AppSettings : IAppSettings, INotifyPropertyChanged
{
    private const string ExamplesCountKey = "examples_count";
    private const string ShowAlertDialogKey = "show_alert_dialog";
    
    private const int MinExamplesCountInternal = 5;
    private const int DefaultExamplesCount = 10;
    private const int MaxExamplesCountInternal = 20;
    private const bool DefaultShowAlertDialog = true;
    
    [Range(MinExamplesCountInternal, MaxExamplesCountInternal)]
    public int ExamplesCount
    {
        get => Preferences.Get(ExamplesCountKey, DefaultExamplesCount);
        set
        {
            Preferences.Set(ExamplesCountKey, value);
            OnPropertyChanged();
        }
    }
    
    public int MaxExamplesCount => MaxExamplesCountInternal;
    public int MinExamplesCount => MinExamplesCountInternal;
    
    public bool ShowAlertDialog
    {
        get => Preferences.Get(ShowAlertDialogKey, DefaultShowAlertDialog);
        set
        {
            Preferences.Set(ShowAlertDialogKey, value);
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