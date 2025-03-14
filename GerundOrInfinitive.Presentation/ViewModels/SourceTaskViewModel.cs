using GerundOrInfinitive.Domain.Models.Teaching;
using GerundOrInfinitive.Presentation.ViewModels.Base;

namespace GerundOrInfinitive.Presentation.ViewModels;

public class SourceTaskViewModel
{
    private readonly SourceTask _sourceTask;
    
    public string BeforeBlankText { get; }
    public string AfterBlankText { get; }

    public SourceTaskViewModel(SourceTask sourceTask)
    {
        _sourceTask = sourceTask;

        string[] substrings = _sourceTask.SourceSentence.Split("...");
        BeforeBlankText = substrings[0];
        AfterBlankText = substrings[1];
    }
}