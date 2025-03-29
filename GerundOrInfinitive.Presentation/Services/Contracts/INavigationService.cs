namespace GerundOrInfinitive.Presentation.Services.Contracts;

public interface INavigationService
{
    public void Initialize(NavigationPage navigationPage);
    public Task NavigateToAsync(Page page);
    public Task GoBackAsync();
}