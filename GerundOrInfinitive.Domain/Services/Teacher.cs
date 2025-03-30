using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Settings;
using GerundOrInfinitive.Domain.Models.Teaching;

namespace GerundOrInfinitive.Domain.Services;

public class Teacher
{
    private readonly IAppSettings _appSettings;
    private readonly ExampleRepository _exampleRepository;
    private List<Example> _examples;

    public Teacher(IAppSettings appSettings, ExampleRepository exampleRepository)
    {
        _exampleRepository = exampleRepository;
        _appSettings = appSettings;
    }
    
    public async Task<IEnumerable<SourceTask>> GenerateTasksAsync()
    {
        _examples ??= await _exampleRepository.GetExamplesAsync(_appSettings.ExamplesCount);

        return _examples.Select(example => new SourceTask(example.Id, example.SourceSentence, example.UsedWord));
    }

    public async Task<IEnumerable<CheckedTask>> CheckAnsweredTasksAsync(IEnumerable<AnsweredTask> answeredTasks)
    {
        if (_examples == null)
        {
            return new[] { CheckedTask.Invalid };       // TODO ???
        }

        return answeredTasks.Select(CheckTask);
    }

    private CheckedTask CheckTask(AnsweredTask answeredTask)
    {
        Example foundExample = _examples.Find(example => example.Id == answeredTask.SourceTask.TaskId);

        if (foundExample != null)
        {
            return new CheckedTask(
                answeredTask.SourceTask, 
                answeredTask.UserAnswer, 
                foundExample.CorrectAnswer, 
                null);
        }
        else
        {
            return CheckedTask.Invalid;
        }
    }
}