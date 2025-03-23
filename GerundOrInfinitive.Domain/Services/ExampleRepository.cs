using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using SQLite;

namespace GerundOrInfinitive.Domain.Services;

public class ExampleRepository
{
    private readonly SQLiteAsyncConnection _database;
    
    public ExampleRepository(string databasePath)
    {
        _database = new SQLiteAsyncConnection(databasePath);
    }

    public Task<List<Example>> GetAllExamplesAsync()
    {
        return _database.Table<Example>().Take(15).ToListAsync();
    }
}