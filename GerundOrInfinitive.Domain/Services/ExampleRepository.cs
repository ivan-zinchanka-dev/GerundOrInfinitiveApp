﻿using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Settings;
using SQLite;

namespace GerundOrInfinitive.Domain.Services;

public class ExampleRepository
{
    private readonly AppSettings _appSettings;
    private readonly SQLiteAsyncConnection _database;
    
    public ExampleRepository(AppSettings appSettings)
    {
        _appSettings = appSettings;
        _database = new SQLiteAsyncConnection(_appSettings.DatabasePath);
    }

    public Task<List<Example>> GetExamplesAsync(int examplesCount)
    {
        return _database.Table<Example>().Take(examplesCount).ToListAsync();
    }
}