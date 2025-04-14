using System.Globalization;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Converters;

// TODO Colors to resources, list item too
public class CheckingStatusToBackgroundConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CheckingStatus checkingStatus)
        {
            switch (checkingStatus)
            {
                case CheckingStatus.Unchecked:
                default:
                    return Colors.White;  
                
                case CheckingStatus.Correct:
                    return Color.FromArgb("#bff199");   
                
                case CheckingStatus.Incorrect:
                    return Color.FromArgb("#f7c8c9");
            }
        }
        else
        {
            throw new InvalidOperationException($"Failed to convert type {targetType}.");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException($"Inverse conversion is not supported.");
    }
}