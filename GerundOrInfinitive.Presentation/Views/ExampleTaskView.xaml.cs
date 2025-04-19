using System.Text.RegularExpressions;
using Android.OS;

namespace GerundOrInfinitive.Presentation.Views;

public partial class ExampleTaskView : ContentView
{
    public static readonly BindableProperty BeforeBlankTextProperty = BindableProperty.Create(
        nameof(BeforeBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.OneWay,
        propertyChanged: OnTextChanged);

    public static readonly BindableProperty InputBlankTextProperty = BindableProperty.Create(
        nameof(InputBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.TwoWay);
    
    public static readonly BindableProperty AfterBlankTextProperty = BindableProperty.Create(
        nameof(AfterBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.OneWay,
        propertyChanged: OnTextChanged);
    
    public static readonly BindableProperty IsReadonlyProperty = BindableProperty.Create(
        nameof(IsReadonly), typeof(bool), typeof(ExampleTaskView), false, BindingMode.OneWay,
        propertyChanged: OnReadonlyStateChanged);
    
    private const int SourcePropertiesCount = 2;
    private int _changedPropertiesCount = 0;
    
    public string BeforeBlankText
    {
        get => (string)GetValue(BeforeBlankTextProperty);
        set => SetValue(BeforeBlankTextProperty, value);
    }

    public string AfterBlankText
    {
        get => (string)GetValue(AfterBlankTextProperty);
        set => SetValue(AfterBlankTextProperty, value);
    }

    public string InputBlankText
    {
        get => (string)GetValue(InputBlankTextProperty);
        set => SetValue(InputBlankTextProperty, value);
    }
    
    public bool IsReadonly
    {
        get => (bool)GetValue(IsReadonlyProperty);
        set => SetValue(IsReadonlyProperty, value);
    }
    
    public ExampleTaskView()
    {
        InitializeComponent();
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ExampleTaskView view)
        {
            view.OnTextPropertyChanged();
        }
    }
    
    private static void OnReadonlyStateChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is ExampleTaskView view)
        {
            view.OnReadonlyStateChanged();
        }
    }

    private void OnTextPropertyChanged()
    {
        _changedPropertiesCount++;

        if (_changedPropertiesCount == SourcePropertiesCount)
        {
            UpdateLayout();
            _changedPropertiesCount = 0;
        }
    }

    private void UpdateLayout()
    {
        _layout.Children.Clear();

        Label[] beforeBlankWordLabels = CreateWordLabels(BeforeBlankText);
        foreach (Label wordLabel in beforeBlankWordLabels)
        {
            _layout.Children.Add(wordLabel);
        }
        
        Entry blankEntry = CreateBlankEntry();
        blankEntry.SetBinding(Entry.TextProperty, 
            new Binding(nameof(InputBlankText), BindingMode.TwoWay, source: this));

        _layout.Children.Add(blankEntry);

        Label[] afterBlankWordLabels = CreateWordLabels(AfterBlankText);
        foreach (Label wordLabel in afterBlankWordLabels)
        {
            _layout.Children.Add(wordLabel);
        }
    }

    private void OnReadonlyStateChanged()
    {
        IEnumerable<Entry> entries = _layout.Children.OfType<Entry>();

        foreach (Entry entry in entries)
        {
            entry.IsReadOnly = IsReadonly;
        }
    }

    private Label[] CreateWordLabels(string sourceText)
    {
        const string splitPattern = @"\S+\s*";
        
        return Regex.Matches(sourceText, splitPattern)
            .Select(match => CreateWordLabel(match.Value))
            .ToArray();
    }

    private Label CreateWordLabel(string text)
    {
        return new Label()
        {
            Text = text,
            LineBreakMode = LineBreakMode.WordWrap,
            VerticalOptions = LayoutOptions.Center
        };
    }

    private Entry CreateBlankEntry()
    {
        var entry = new Entry()
        {
            WidthRequest = 80d,
            VerticalOptions = LayoutOptions.Center
        };
        
        return entry;
    }

}