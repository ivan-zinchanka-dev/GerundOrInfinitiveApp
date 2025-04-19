using System.Globalization;
using GerundOrInfinitive.Domain.Models.ExampleTask;

namespace GerundOrInfinitive.Presentation.Converters;

public class CheckingStatusToReadonlyConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CheckingStatus checkingStatus)
        {
            switch (checkingStatus)
            {
                case CheckingStatus.Unchecked:
                default:
                    return false;  
                
                case CheckingStatus.Correct:
                case CheckingStatus.Incorrect:
                    return true;
            }
        }
        else
        {
            throw new InvalidOperationException($"Failed to convert type {targetType}.");
        }
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("Inverse conversion is not supported.");
    }
}