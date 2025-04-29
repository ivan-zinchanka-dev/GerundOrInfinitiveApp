using System.Globalization;
using GerundOrInfinitive.Domain.Models.ExampleTask;

namespace GerundOrInfinitive.Presentation.Converters;

public class CheckingStatusToBackgroundConverter : IValueConverter
{
    public Color UncheckedColor { get; set; } = Colors.White;
    public Color CorrectColor { get; set; } = Colors.LightGreen;
    public Color IncorrectColor { get; set; } = Colors.LightCoral;
    
    public Color UncheckedColorDark { get; set; } = Colors.DimGrey;
    public Color CorrectColorDark { get; set; } = Colors.Lime;
    public Color IncorrectColorDark { get; set; } = Colors.Coral;
    
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is CheckingStatus checkingStatus)
        {
            switch (checkingStatus)
            {
                case CheckingStatus.Unchecked:
                default:
                    return GetColorByTheme(UncheckedColor, UncheckedColorDark);  
                
                case CheckingStatus.Correct:
                    return GetColorByTheme(CorrectColor, CorrectColorDark);    
                
                case CheckingStatus.Incorrect:
                    return GetColorByTheme(IncorrectColor, IncorrectColorDark);  
            }
        }
        else
        {
            throw new InvalidOperationException($"Failed to convert type {targetType}.");
        }
    }

    private Color GetColorByTheme(Color light, Color dark)
    {
        return Application.Current?.RequestedTheme == AppTheme.Dark ? dark : light;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new InvalidOperationException("Inverse conversion is not supported.");
    }
}