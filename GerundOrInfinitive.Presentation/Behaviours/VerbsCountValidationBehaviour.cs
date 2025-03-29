namespace GerundOrInfinitive.Presentation.Behaviours;

public class VerbsCountValidationBehaviour : Behavior<Entry>
{
    public static readonly BindableProperty ErrorMessageProperty = BindableProperty.Create(
        nameof(ErrorMessage), 
        typeof(string), 
        typeof(VerbsCountValidationBehaviour), 
        string.Empty, 
        BindingMode.TwoWay);

    public string ErrorMessage
    {
        get => (string)GetValue(ErrorMessageProperty);
        set => SetValue(ErrorMessageProperty, value);
    }
    
    
    protected override void OnAttachedTo(Entry entry)
    {
        entry.TextChanged += OnTextChanged;
        base.OnAttachedTo(entry);
    }

    protected override void OnDetachingFrom(Entry entry)
    {
        entry.TextChanged -= OnTextChanged;
        base.OnDetachingFrom(entry);
    }

    private void OnTextChanged(object sender, TextChangedEventArgs eventArgs)
    {
        if (int.TryParse(eventArgs.NewTextValue, out int verbsCount))
        {
            
        }
        else
        {
            ErrorMessage = "Please enter a number";
        }
    }
}