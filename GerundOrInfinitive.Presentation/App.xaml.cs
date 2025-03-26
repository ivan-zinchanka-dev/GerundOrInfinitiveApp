using GerundOrInfinitive.Presentation.Views;

namespace GerundOrInfinitive.Presentation;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
        
        MainPage = new NavigationPage(new MainPage());
    }
}