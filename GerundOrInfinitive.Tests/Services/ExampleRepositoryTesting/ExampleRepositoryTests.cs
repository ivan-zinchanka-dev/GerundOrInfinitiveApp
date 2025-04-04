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
            ExampleId = 15,
            Result = true,
            Time = DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
        });
        
        Assert.IsTrue(success);
    }


}
