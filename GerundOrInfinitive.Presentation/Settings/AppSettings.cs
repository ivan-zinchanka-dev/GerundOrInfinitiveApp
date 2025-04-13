using System.ComponentModel.DataAnnotations;
using GerundOrInfinitive.Domain.Models.Settings;
using ReactiveUI;

namespace GerundOrInfinitive.Presentation.Settings;

internal class AppSettings : ReactiveObject, IAppSettings
{
    private const string ExamplesCountKey = "examples_count";
    private const string ShowAlertDialogKey = "show_alert_dialog";
    
    private const int MinExamplesCountInternal = 5;
    private const int DefaultExamplesCount = 10;
    private const int MaxExamplesCountInternal = 20;
    private const bool DefaultShowAlertDialog = true;

    private int _examplesCount;
    private bool _showAlertDialog;
    
    [Range(MinExamplesCountInternal, MaxExamplesCountInternal)]
    public int ExamplesCount
    {
        get => _examplesCount;
        set => this.RaiseAndSetIfChanged(ref _examplesCount, value);
    }
    
    public int MaxExamplesCount => MaxExamplesCountInternal;
    public int MinExamplesCount => MinExamplesCountInternal;
    
    public bool ShowAlertDialog
    {
        get => _showAlertDialog;
        set => this.RaiseAndSetIfChanged(ref _showAlertDialog, value);
    }
    
    public string DatabasePath { get; }

    public AppSettings(string databasePath)
    {
        DatabasePath = databasePath;
        
        _examplesCount = Preferences.Get(ExamplesCountKey, DefaultExamplesCount);
        this.WhenAnyValue(model => model.ExamplesCount)
            .Subscribe(value => Preferences.Set(ExamplesCountKey, value));
        
        _showAlertDialog = Preferences.Get(ShowAlertDialogKey, DefaultShowAlertDialog);
        this.WhenAnyValue(model => model.ShowAlertDialog)
            .Subscribe(value =>  Preferences.Set(ShowAlertDialogKey, value));
    }
}