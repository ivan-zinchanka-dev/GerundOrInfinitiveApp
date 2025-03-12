using System.Diagnostics;

namespace GerundOrInfinitive.Presentation;

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
}