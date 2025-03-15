using System.Diagnostics;
using GerundOrInfinitive.Domain.Models.DataBaseObjects;

namespace GerundOrInfinitive.Domain.Services;

public class DatabaseService
{
    //private readonly SQLiteAsyncConnection _database;

    public DatabaseService(string dbPath)
    {
        //_database = new SQLiteAsyncConnection(dbPath);
        
        Debug.WriteLine("DbPath: " + dbPath);
        Console.WriteLine("DbPath: " + dbPath);
    }

    /*public async Task<List<Example>> GetExamplesAsync()
    {
        return await _database.Table<Example>().ToListAsync();
    }*/
}