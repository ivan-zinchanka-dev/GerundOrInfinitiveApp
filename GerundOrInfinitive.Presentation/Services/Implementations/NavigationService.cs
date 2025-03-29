using GerundOrInfinitive.Presentation.Services.Contracts;
using Microsoft.Extensions.Logging;

namespace GerundOrInfinitive.Presentation.Services.Implementations;

public class NavigationService : INavigationService
{
    private readonly ILogger<NavigationService> _logger;
    private readonly IServiceProvider _serviceProvider;
    private NavigationPage _navigationPage;

    public NavigationService(ILogger<NavigationService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public void Initialize(NavigationPage navigationPage)
    {
        _navigationPage = navigationPage;
    }

    public async Task<bool> NavigateToAsync<TPage>() where TPage : Page
    {
        Type pageType = typeof(TPage);

        if (_navigationPage != null)
        {
            TPage page = _serviceProvider.GetService<TPage>();

            if (page != null)
            {
                _logger.LogInformation("Navigating to page of type '{0}'", pageType.ToString());
                await _navigationPage.PushAsync(page, true);
                _logger.LogInformation("Navigation succeeded");
                return true;
            }
            else
            {
                _logger.LogWarning("Page of type'{0}' not found", pageType.ToString());
            }
        }
        else
        {
            _logger.LogWarning("Navigation service not initialized");
        }

        return false;
    }
    
    public async Task<bool> GoBackAsync()
    {
        if (_navigationPage?.Navigation?.NavigationStack.Count > 1)
        {
            _logger.LogInformation("Navigating back");
            await _navigationPage.PopAsync(true);
            _logger.LogInformation("Navigation succeeded");
            return true;
        }
        else
        {
            _logger.LogWarning("Navigation service not initialized");
        }

        return false;
    }
}