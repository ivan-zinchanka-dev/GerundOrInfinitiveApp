namespace GerundOrInfinitive.Presentation.Views;

public partial class ExampleTaskView : ContentView
{
    public static readonly BindableProperty BeforeBlankTextProperty = BindableProperty.Create(
        nameof(BeforeBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.OneWay);

    public static readonly BindableProperty AfterBlankTextProperty = BindableProperty.Create(
        nameof(AfterBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.OneWay);

    public static readonly BindableProperty InputBlankTextProperty = BindableProperty.Create(
        nameof(InputBlankText), typeof(string), typeof(ExampleTaskView), string.Empty, BindingMode.TwoWay);

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
    
    public ExampleTaskView()
    {
        InitializeComponent();
        
        _beforeBlankLabel.SetBinding(Label.TextProperty, 
            new Binding(nameof(BeforeBlankText), BindingMode.OneWay, source: this));
        
        _inputBlankEntry.SetBinding(Entry.TextProperty, 
            new Binding(nameof(InputBlankText), BindingMode.TwoWay, source: this));
    
        _afterBlankLabel.SetBinding(Label.TextProperty, 
            new Binding(nameof(AfterBlankText), BindingMode.OneWay, source: this));
        
    }
}