using Microsoft.Maui.Controls.Internals;

namespace GerundOrInfinitive.Presentation.Services.Implementations;

internal class AppResources
{
    private const string TutorialResourceKey = "Tutorial";
    private const string CheckingResultResourceKey = "CheckingResult";
    
    private IResourceDictionary _resources;
    
    private IResourceDictionary Resources
    {
        get
        {
            return _resources ??= Application.Current?.Resources;
        }
    }

    private string GetString(string key)
    {
        if (Resources == null)
        {
            return null;
        }

        if (Resources.TryGetValue(key, out object obj) && obj is string str)
        {
            return str;
        }
        else
        {
            return null;
        }
    }
    
    public string TutorialString => GetString(TutorialResourceKey);
    public string CheckingResultString => GetString(CheckingResultResourceKey);
}