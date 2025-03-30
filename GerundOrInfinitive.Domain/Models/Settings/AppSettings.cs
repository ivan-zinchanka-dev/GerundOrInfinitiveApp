﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GerundOrInfinitive.Domain.Models.Settings;

public class AppSettings : INotifyPropertyChanged
{
    private int _examplesCount = MinExamplesCount;
    
    public const int MinExamplesCount = 5;
    public const int MaxExamplesCount = 20;
    
    [Range(MinExamplesCount, MaxExamplesCount)]
    public int ExamplesCount
    {
        get => _examplesCount;
        set
        {
            _examplesCount = value;
            OnPropertyChanged();
        }
    }
    
    public string DatabasePath { get; }

    public AppSettings(string databasePath)
    {
        DatabasePath = databasePath;
    }

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}