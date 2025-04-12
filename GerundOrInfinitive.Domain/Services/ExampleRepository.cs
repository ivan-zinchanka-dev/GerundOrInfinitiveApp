using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Settings;
using SQLite;

namespace GerundOrInfinitive.Domain.Services;

public class ExampleRepository
{
    private const string GetExamplesQuery = "SELECT * FROM ExamplesBatch LIMIT {0};";
    
    private readonly IAppSettings _appSettings;
    private readonly SQLiteAsyncConnection _database;
    
    private List<AlternativeCorrectAnswer> _alternativeCorrectAnswers;
    
    public ExampleRepository(IAppSettings appSettings)
    {
        _appSettings = appSettings;
        _database = new SQLiteAsyncConnection(_appSettings.DatabasePath);
    }
    
    internal async Task<IReadOnlyList<Example>> GetExamplesBatchAsync(int examplesCount)
    {
        _alternativeCorrectAnswers ??= await GetAlternativeAnswersAsync();

        string query = string.Format(GetExamplesQuery, examplesCount);
        
        List<Example> examples = await _database.QueryAsync<Example>(query);
        examples.ForEach(IncludeAlternativeAnswerIfNeed);
        return examples;
    }

    internal async Task<bool> AddResponseAsync(LatestExampleResponse newResponse)
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
    
    private void IncludeAlternativeAnswerIfNeed(Example example)
    { 
        AlternativeCorrectAnswer answer = _alternativeCorrectAnswers
            .Find(answer => answer.ExampleId == example.Id);

        if (answer != null)
        {
            example.AlternativeCorrectAnswer = answer;
        }
    }
    
    private async Task<List<AlternativeCorrectAnswer>> GetAlternativeAnswersAsync()
    {
        return await _database.Table<AlternativeCorrectAnswer>().ToListAsync();
    }
}