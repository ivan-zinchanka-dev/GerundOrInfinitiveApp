using System.Diagnostics;
using GerundOrInfinitive.Domain;
using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnStartClick(object sender, EventArgs eventArgs)
    {
        await Navigation.PushAsync(new TestingPage(new TestingViewModel()), true);
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