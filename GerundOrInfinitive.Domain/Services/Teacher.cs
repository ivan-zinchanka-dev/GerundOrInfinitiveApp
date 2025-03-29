using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Teaching;

namespace GerundOrInfinitive.Domain.Services;

public class Teacher
{
    private readonly ExampleRepository _exampleRepository;
    private List<Example> _examples;

    public Teacher(ExampleRepository exampleRepository)
    {
        _exampleRepository = exampleRepository;
    }
    

    public async Task<IEnumerable<SourceTask>> GenerateTasksAsync()
    {
        _examples ??= await _exampleRepository.GetAllExamplesAsync();

        return _examples.Select(example => new SourceTask(example.Id, example.SourceSentence, example.UsedWord));
    }

    public async Task<IEnumerable<CheckedTask>> CheckAnsweredTasksAsync(IEnumerable<AnsweredTask> answeredTasks)
    {
        _examples ??= await _exampleRepository.GetAllExamplesAsync();
        
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