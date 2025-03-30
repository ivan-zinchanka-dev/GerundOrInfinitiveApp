using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Settings;
using SQLite;

namespace GerundOrInfinitive.Domain.Services;

public class ExampleRepository
{
    private readonly IAppSettings _appSettings;
    private readonly SQLiteAsyncConnection _database;
    
    public ExampleRepository(IAppSettings appSettings)
    {
        _appSettings = appSettings;
        _database = new SQLiteAsyncConnection(_appSettings.DatabasePath);
    }

    /*public Task<List<Example>> GetExamplesAsync(int examplesCount)
    {
        return _database.Table<Example>()
            //.OrderBy(example => Guid.NewGuid()) 
            .Take(examplesCount)
            .ToListAsync();
    }*/
    
    public async Task<List<Example>> GetExamplesAsync(int examplesCount)
    {
        List<Example> examples = 
            await _database.QueryAsync<Example>($"SELECT * FROM Examples ORDER BY RANDOM() LIMIT {examplesCount}");

        foreach (Example example in examples)
        {
            example.AlternativeCorrectAnswer = await GetAlternativeAnswerAsync(example.Id);
        }

        return examples;
    }

    private async Task<AlternativeCorrectAnswer> GetAlternativeAnswerAsync(int exampleId)
    {
        return await _database.Table<AlternativeCorrectAnswer>()
            .Where(answer => answer.ExampleId == exampleId)
            .FirstOrDefaultAsync();
    }
}