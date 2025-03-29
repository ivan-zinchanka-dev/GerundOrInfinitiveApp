namespace GerundOrInfinitive.Presentation.Services.Implementations;

// TODO Use INavigationService and ILogger
public class NavigationService /*: INavigationService*/
{
    private readonly IServiceProvider _serviceProvider;
    private NavigationPage _navigationPage;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void Initialize(NavigationPage navigationPage)
    {
        _navigationPage = navigationPage;
    }

    public async Task<bool> NavigateToAsync<TPage>() where TPage : Page
    {
        if (_navigationPage != null)
        {
            TPage page = _serviceProvider.GetService<TPage>();

            if (page != null)
            {
                await _navigationPage.PushAsync(page, true);
                return true;
            }
        }

        return false;
    }

    public async Task<bool> GoBackAsync()
    {
        if (_navigationPage?.Navigation?.NavigationStack.Count > 1)
        {
            await _navigationPage.PopAsync(true);
            return true;
        }

        return false;
    }
}