using System.Diagnostics;
using GerundOrInfinitive.Domain;

namespace GerundOrInfinitive.Presentation.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void OnStartClick(object sender, EventArgs eventArgs)
    {
        Console.WriteLine("Console testing");
        Debug.WriteLine("Debug testing");
        
        Console.WriteLine(new TestService().Get());
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