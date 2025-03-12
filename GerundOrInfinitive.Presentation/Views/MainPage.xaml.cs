using System.Diagnostics;

namespace GerundOrInfinitive.Presentation.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnStartClick(object sender, EventArgs eventArgs)
    {
        Console.WriteLine("Start testing");
        Debug.WriteLine("Start testing");
    }

    private async void OnSettingsClick(object sender, EventArgs eventArgs)
    {
        try
        {
            await Navigation.PushAsync(new SettingsPage(), true);
            
            
        }
        catch (Exception e)
        {
            
        }
    }
}