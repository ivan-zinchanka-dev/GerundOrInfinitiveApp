﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace GerundOrInfinitive.Domain.Models.Settings;

public class AppSettings : INotifyPropertyChanged
{
    private int _verbsCount;

    [Range(1, int.MaxValue)]
    public int VerbsCount
    {
        get => _verbsCount;
        set
        {
            _verbsCount = value;
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