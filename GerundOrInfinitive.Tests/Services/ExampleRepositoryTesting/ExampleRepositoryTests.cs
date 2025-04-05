using GerundOrInfinitive.Domain.Models.DataBaseObjects;
using GerundOrInfinitive.Domain.Services;
using GerundOrInfinitive.Tests.Settings;

namespace GerundOrInfinitive.Tests.Services.ExampleRepositoryTesting;

[TestFixture]
public class ExampleRepositoryTests
{
    private ExampleRepository _exampleRepository;
    
    [SetUp]
    public void Setup()
    {
        _exampleRepository = new ExampleRepository(new MockAppSettings());
    }

    [Test]
    public void ShowCurDir()
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }
    
    [Test]
    public async Task GetExamplesAsync()
    {
        var l = await _exampleRepository.GetExamplesBatchAsync(10);

        foreach (var ex in l)
        {
            Console.WriteLine(ex.Id);
        }
    }
    
    [Test]
    public async Task AddResponse()
    {
        bool success = await _exampleRepository.AddResponseAsync(new LatestExampleResponse()
        {
            ExampleId = 10,
            Result = false,
            Time = DateTime.UtcNow,
        });
        
        success &= await _exampleRepository.AddResponseAsync(new LatestExampleResponse()
        {
            ExampleId = 40,
            Result = false,
            Time = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
        });
        
        success &= await _exampleRepository.AddResponseAsync(new LatestExampleResponse()
        {
            ExampleId = 30,
            Result = true,
            Time = DateTime.UtcNow.Subtract(TimeSpan.FromDays(3)),
        });
        
        success &= await _exampleRepository.AddResponseAsync(new LatestExampleResponse()
        {
            ExampleId = 15,
            Result = true,
            Time = DateTime.UtcNow.Subtract(TimeSpan.FromDays(1)),
        });
        
        Assert.IsTrue(success);
    }
}
