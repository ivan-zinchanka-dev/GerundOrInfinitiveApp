using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Models.Teaching;

namespace GerundOrInfinitive.Domain.Services;

public class Teacher
{
    private static readonly List<Example> Examples = new List<Example>()
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
            SourceSentence = "In court the accused admitted (to) ... the documents.",
            UsedWord = "steal",
            CorrectAnswer = "stealing"
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
            AlternativeCorrectAnswer = "find"
        },
    };
    
    
    public IEnumerable<ExampleTask> GenerateTasks()
    {
        return Examples.Select(example => new ExampleTask(example.Id, example.SourceSentence, example.UsedWord));
    }

    public IEnumerable<CheckedTask> CheckAnsweredTasks(IEnumerable<AnsweredTask> answeredTasks)
    {
        return answeredTasks.Select(CheckTask);
    }

    private CheckedTask CheckTask(AnsweredTask answeredTask)
    {
        Example foundExample = Examples.Find(example => example.Id == answeredTask.ExampleTask.TaskId);

        if (foundExample != null)
        {
            return new CheckedTask(
                answeredTask.ExampleTask, 
                answeredTask.UserAnswer, 
                foundExample.CorrectAnswer, 
                foundExample.AlternativeCorrectAnswer);
        }
        else
        {
            return CheckedTask.Invalid;
        }
    }
}