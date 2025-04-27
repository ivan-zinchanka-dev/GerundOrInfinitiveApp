namespace GerundOrInfinitive.Presentation.Views.Controls;

public partial class Stepper : ContentView
{
	private int _digits = 4;
	
	public static readonly BindableProperty MaximumProperty = BindableProperty.Create(nameof(Maximum), typeof(double), typeof(Stepper), 100.0,
		validateValue: (bindable, value) => (double)value > ((Stepper)bindable).Minimum,
		coerceValue: (bindable, value) =>
		{
			var stepper = (Stepper)bindable;
			stepper.Value = Math.Clamp((double)value, stepper.Minimum, stepper.Maximum);
			return value;
		});
	
	public static readonly BindableProperty MinimumProperty = BindableProperty.Create(nameof(Minimum), typeof(double), typeof(Stepper), 0.0,
		validateValue: (bindable, value) => (double)value < ((Stepper)bindable).Maximum,
		coerceValue: (bindable, value) =>
		{
			var stepper = (Stepper)bindable;
			stepper.Value = Math.Clamp((double)value, stepper.Minimum, stepper.Maximum);
			return value;
		});
	
	public static readonly BindableProperty ValueProperty = BindableProperty.Create(nameof(Value), typeof(double), typeof(Stepper), 0.0, BindingMode.TwoWay,
		coerceValue: (bindable, value) =>
		{
			var stepper = (Stepper)bindable;
			double roundValue = Math.Round((double)value, stepper._digits);
			return Math.Clamp(roundValue, stepper.Minimum, stepper.Maximum);
		},
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			var stepper = (Stepper)bindable;
			stepper.ValueChanged?.Invoke(stepper, new ValueChangedEventArgs((double)oldValue, (double)newValue));
		});
	
	public static readonly BindableProperty IncrementProperty = BindableProperty.Create(nameof(Increment), typeof(double), typeof(Stepper), 1.0,
		propertyChanged: OnIncrementPropertyChanged);
	
	public double Increment
	{
		get => (double)GetValue(IncrementProperty);
		set => SetValue(IncrementProperty, value);
	}

	public double Maximum
	{
		get => (double)GetValue(MaximumProperty);
		set => SetValue(MaximumProperty, value);
	}

	public double Minimum
	{
		get => (double)GetValue(MinimumProperty);
		set => SetValue(MinimumProperty, value);
	}

	public double Value
	{
		get => (double)GetValue(ValueProperty);
		set => SetValue(ValueProperty, value);
	}

	public event EventHandler<ValueChangedEventArgs> ValueChanged;
	
	public Stepper()
    {
        InitializeComponent();
    }
    
	public Stepper(double min, double max, double value, double increment) : this()
	{
		if (min >= max)
			throw new ArgumentOutOfRangeException(nameof(min));
		if (max > Minimum)
		{
			Maximum = max;
			Minimum = min;
		}
		else
		{
			Minimum = min;
			Maximum = max;
		}

		Increment = increment;
		Value = Math.Clamp(value, min, max);
	}
	
	private static void OnIncrementPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
		const int minDigits = 1;
		const int digitsAddend = 4;
		const int maxDigits = 15;
		
		((Stepper)bindable)._digits = 
			Math.Clamp((int)(-Math.Log10((double)newValue) + digitsAddend), minDigits, maxDigits);
	}
	
}