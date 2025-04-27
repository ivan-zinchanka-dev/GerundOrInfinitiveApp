namespace GerundOrInfinitive.Presentation.Views.Controls;

public partial class Stepper : ContentView
{
	private static class RoundingDigits
	{
		public const int Min = 1;
		public const int Default = 4;
		public const int Max = 15;
	}
	
	private static class Defaults
	{
		public const double Max = 100.0;
		public const double Min = 0.0;
		public const double Increment = 1.0;
	}
	
	private int _roundingDigits = RoundingDigits.Default;
	
	public static readonly BindableProperty MaximumProperty = BindableProperty.Create(
		nameof(Maximum), typeof(double), typeof(Stepper), Defaults.Max,
		validateValue: (bindable, value) => (double)value > ((Stepper)bindable).Minimum);
	
	public static readonly BindableProperty MinimumProperty = BindableProperty.Create(
		nameof(Minimum), typeof(double), typeof(Stepper), Defaults.Min,
		validateValue: (bindable, value) => (double)value < ((Stepper)bindable).Maximum);
	
	public static readonly BindableProperty ValueProperty = BindableProperty.Create(
		nameof(Value), typeof(double), typeof(Stepper), Defaults.Min, BindingMode.TwoWay,
		coerceValue: (bindable, value) =>
		{
			var stepper = (Stepper)bindable;
			double roundValue = Math.Round((double)value, stepper._roundingDigits);
			return Math.Clamp(roundValue, stepper.Minimum, stepper.Maximum);
		},
		propertyChanged: (bindable, oldValue, newValue) =>
		{
			var stepper = (Stepper)bindable;
			stepper.ValueChanged?.Invoke(stepper, new ValueChangedEventArgs((double)oldValue, (double)newValue));
		});
	
	public static readonly BindableProperty IncrementProperty = BindableProperty.Create(
		nameof(Increment), typeof(double), typeof(Stepper), Defaults.Increment,
		propertyChanged: OnIncrementPropertyChanged);
	
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
	
	public double Increment
	{
		get => (double)GetValue(IncrementProperty);
		set => SetValue(IncrementProperty, value);
	}

	public event EventHandler<ValueChangedEventArgs> ValueChanged;
	
	public Stepper()
    {
        InitializeComponent();
        
        ValueChanged += UpdateButtonStates;
        Unloaded += OnUnloaded;
    }

	private void UpdateButtonStates(double value)
	{
		_minusButton.IsEnabled = value > Minimum;
		_plusButton.IsEnabled = value < Maximum;
	}
	
	private void UpdateButtonStates(object sender, ValueChangedEventArgs eventArgs)
	{
		UpdateButtonStates(eventArgs.NewValue);
	}
	
	private static void OnIncrementPropertyChanged(BindableObject bindable, object oldValue, object newValue)
	{
		((Stepper)bindable)._roundingDigits = 
			Math.Clamp((int)(-Math.Log10((double)newValue) + RoundingDigits.Default), 
				RoundingDigits.Min, RoundingDigits.Max);
	}

	private void OnMinusButtonClick(object sender, EventArgs eventArgs)
	{
		Value -= Increment;
	}
	
	private void OnPlusButtonClick(object sender, EventArgs eventArgs)
	{
		Value += Increment;
	}
	
	private void OnUnloaded(object sender, EventArgs eventArgs)
	{
		ValueChanged -= UpdateButtonStates;
		Unloaded -= OnUnloaded;
	}
}