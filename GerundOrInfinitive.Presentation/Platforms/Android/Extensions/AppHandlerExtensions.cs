using Android.Views;
using Android.Widget;
using AndroidX.RecyclerView.Widget;
using Microsoft.Maui.Controls.Handlers.Items;
using Color = Android.Graphics.Color;


namespace GerundOrInfinitive.Presentation.Extensions;

internal static class AppHandlerExtensions
{
    private const string EdgeEffectColorKey = "EdgeEffectColor";
    private static readonly Color EdgeEffectColor = Color.ParseColor("#E22B2B");
    
    public static void AdjustEdgeEffects(this IMauiHandlersCollection handlers)
    {
        handlers.AddHandler<CollectionView, CollectionViewHandler>();
        
        CollectionViewHandler.Mapper.AppendToMapping(EdgeEffectColorKey, (handler, view) =>
        {
            RecyclerView recyclerView = handler.PlatformView;
            recyclerView.OverScrollMode = OverScrollMode.Always;
            recyclerView.SetEdgeEffectFactory(new InternalEdgeEffectFactory(EdgeEffectColor));
        });
    }

    private class InternalEdgeEffectFactory : RecyclerView.EdgeEffectFactory
    {
        private readonly Color _color;
        public InternalEdgeEffectFactory(Color color) => _color = color;

        protected override EdgeEffect CreateEdgeEffect(RecyclerView view, int direction)
        {
            EdgeEffect effect = base.CreateEdgeEffect(view, direction);
            effect.SetColor(_color);
            return effect;
        }
    }
    
}