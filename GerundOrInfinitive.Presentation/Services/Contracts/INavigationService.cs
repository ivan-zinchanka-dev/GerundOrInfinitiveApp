namespace GerundOrInfinitive.Presentation.Services.Contracts;

public interface INavigationService
{
    public void Initialize(NavigationPage navigationPage);
    public Task<bool> NavigateToAsync<TPage>() where TPage : Page;
    public Task<bool> GoBackAsync();
}