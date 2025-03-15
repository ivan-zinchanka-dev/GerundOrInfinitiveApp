using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Teaching;

namespace GerundOrInfinitive.Domain.Services;

public class Teacher
{
    private readonly ExampleRepository _exampleRepository;
    private List<Example> _examples;
    
    /*private static readonly List<Example> Examples = new List<Example>()
    {
        new Example()
        {
            Id = 0,
            SourceSentence = "In court the accused admitted (to) ... the documents.",
            UsedWord = "steal",
            CorrectAnswer = "stealing"
        },
        new Example()
        {
            Id = 1,
            SourceSentence = "I always try to avoid ... in the rush hour.",
            UsedWord = "drive",
            CorrectAnswer = "driving"
        },
        new Example()
        {
            Id = 2,
            SourceSentence = "I can''t afford ... on holiday this summer.",
            UsedWord = "go",
            CorrectAnswer = "to go"
        },
        new Example()
        {
            Id = 3,
            SourceSentence = "The organization I work for helps young people ... work abroad.",
            UsedWord = "find",
            CorrectAnswer = "to find",
        },
    };*/

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