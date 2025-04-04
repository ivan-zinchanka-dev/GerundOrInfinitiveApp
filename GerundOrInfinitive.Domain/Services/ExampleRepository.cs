using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Settings;
using SQLite;

namespace GerundOrInfinitive.Domain.Services;

public class ExampleRepository
{
    private const string GetExamplesQuery = "SELECT * FROM OrderedExamples LIMIT {0};";
    
    private readonly IAppSettings _appSettings;
    private readonly SQLiteAsyncConnection _database;
    
    public ExampleRepository(IAppSettings appSettings)
    {
        _appSettings = appSettings;
        _database = new SQLiteAsyncConnection(_appSettings.DatabasePath);
    }
    
    public async Task<List<Example>> GetExamplesAsync(int examplesCount)
    {
        return await _database.QueryAsync<Example>(
            string.Format(GetExamplesQuery, examplesCount));
    }

    public async Task<bool> AddResponseAsync(LatestExampleResponse newResponse)
    {
        LatestExampleResponse oldResponse = await _database
            .Table<LatestExampleResponse>()
            .Where(oldResponse => oldResponse.ExampleId == newResponse.ExampleId)
            .FirstOrDefaultAsync();

        int rowsCount;
        
        if (oldResponse == null)
        {
            rowsCount = await _database.InsertAsync(newResponse);
        }
        else
        {
            rowsCount = await _database.UpdateAsync(newResponse);
        }
        
        return rowsCount > 0;
    }
    

    /*public async Task<List<Example>> GetExamplesAsync(int examplesCount)
    {


        List<Example> examples =
            await _database.QueryAsync<Example>($"SELECT * FROM Examples ORDER BY RANDOM() LIMIT {examplesCount}");

        foreach (Example example in examples)
        {
            example.AlternativeCorrectAnswer = await GetAlternativeAnswerAsync(example.Id);
        }

        return examples;
    }*/

    //TODO Load all at start for optimization
    private async Task<AlternativeCorrectAnswer> GetAlternativeAnswerAsync(int exampleId)
    {
        return await _database.Table<AlternativeCorrectAnswer>()
            .Where(answer => answer.ExampleId == exampleId)
            .FirstOrDefaultAsync();
    }
}