using Android.Content;
using Android.Content.Res;
using Android.Widget;
using AndroidX.Core.Content;
using Microsoft.Maui.Handlers;
using Color = Android.Graphics.Color;

namespace GerundOrInfinitive.Presentation.Extensions;

public static class AppHandlersExtensions
{
    private const string CustomColorsKey = "CustomColors";
    
    /*private static Color PrimaryColor
    {
        get
        {
            Context context = Android.App.Application.Context;
            Resources resources = context.Resources;

            if (resources != null)
            {
                int colorId = resources.GetIdentifier("my_primary_color", "color", context.PackageName);
                return new Color(ContextCompat.GetColor(context, colorId));
            }
            else
            {
                return default;
            }
        }
    }*/

    public static void AddStepperHandler(this IMauiHandlersCollection handlers)
    {
        handlers.AddHandler<Stepper, StepperHandler>();
        StepperHandler.Mapper.AppendToMapping(CustomColorsKey, (handler, view) =>
        {
            if (handler.PlatformView is LinearLayout layout)
            {
                for (int i = 0; i < layout.ChildCount; i++)
                {
                    if (layout.GetChildAt(i) is Android.Widget.Button button)
                    {
                        button.SetBackgroundColor(Color.ParseColor("#E22B2B"));
                        button.SetTextColor(Color.White);
                    }
                }
            }
        });
    }
}