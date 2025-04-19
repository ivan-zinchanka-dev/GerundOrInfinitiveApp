using System.Globalization;
using GerundOrInfinitive.Domain.Models.ExampleTask;

namespace GerundOrInfinitive.Presentation.Converters;

public class CheckingStatusToBackgroundConverter : IValueConverter
{
    public Color UncheckedColor { get; set; } = Colors.White;
    public Color CorrectColor { get; set; } = Colors.LightGreen;
    public Color IncorrectColor { get; set; } = Colors.LightCoral;
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CheckingStatus checkingStatus)
        {
            switch (checkingStatus)
            {
                case CheckingStatus.Unchecked:
                default:
                    return UncheckedColor;  
                
                case CheckingStatus.Correct:
                    return CorrectColor;   
                
                case CheckingStatus.Incorrect:
                    return IncorrectColor;
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