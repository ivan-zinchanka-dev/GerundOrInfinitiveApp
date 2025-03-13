using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GerundOrInfinitive.Presentation.Views;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        CreateFillInTheBlanksUi();
    }
    
    private void CreateFillInTheBlanksUi()
    {
        // Create a HorizontalStackLayout
        var stackLayout = new HorizontalStackLayout
        {
            Spacing = 5
        };

        // Add the first label
        stackLayout.Children.Add(new Label { Text = "The" });

        // Create an Entry field for user input
        var wordEntry = new Entry
        {
            WidthRequest = 50,
            Placeholder = "_____"
        };

        // Add the Entry field to the layout
        stackLayout.Children.Add(wordEntry);

        // Add the second label
        stackLayout.Children.Add(new Label { Text = "is blue." });

        // Set the dynamically created layout as the page content
        Content = new VerticalStackLayout
        {
            Children = { stackLayout }
        };
    }
}