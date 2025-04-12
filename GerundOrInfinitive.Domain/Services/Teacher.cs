using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Domain.Models.Settings;

namespace GerundOrInfinitive.Domain.Services;

public class Teacher
{
    private readonly IAppSettings _appSettings;
    private readonly ExampleRepository _exampleRepository;
    private List<Example> _examples;

    public IReadOnlyList<ExampleTask> CurrentTasks { get; private set; }

    public Teacher(IAppSettings appSettings, ExampleRepository exampleRepository)
    {
        _exampleRepository = exampleRepository;
        _appSettings = appSettings;
    }
    
    public async Task<IReadOnlyList<ExampleTask>> NewTasksAsync()
    {
        IReadOnlyList<Example> examplesBatch = await _exampleRepository.GetExamplesBatchAsync(_appSettings.ExamplesCount);
        CurrentTasks = examplesBatch.Select(example => new ExampleTask(example)).ToList();
        return CurrentTasks;
    }

    public async Task CheckTasksAsync()
    {
        Task[] responseTasks = new Task[CurrentTasks.Count];

        for (int i = 0; i < CurrentTasks.Count; i++)
        {
            ExampleTask exampleTask = CurrentTasks[i];
            bool result = exampleTask.Check();

            Task responseTask = _exampleRepository.AddResponseAsync(new LatestExampleResponse()
            {
                ExampleId = exampleTask.ExampleId,
                Result = result,
                Time = DateTime.UtcNow
            });

            responseTasks[i] = responseTask;
        }
        
        await Task.WhenAll(responseTasks);
    }
}