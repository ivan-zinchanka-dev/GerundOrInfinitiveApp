using System.Globalization;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Presentation.ViewModels;

namespace GerundOrInfinitive.Presentation.Converters;

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
                    return Colors.WhiteSmoke;  
                
                case CheckingStatus.Correct:
                    return Colors.LightGreen;   
                
                case CheckingStatus.Incorrect:
                    return Colors.LightCoral;  
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