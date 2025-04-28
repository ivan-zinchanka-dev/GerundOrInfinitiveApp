using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text.RegularExpressions;
using GerundOrInfinitive.Domain.Models.ExampleTask;
using GerundOrInfinitive.Presentation.ViewModels;
using ReactiveUI;
using ReactiveUI.Maui;

namespace GerundOrInfinitive.Presentation.Views;

public partial class ExampleTaskView : ReactiveContentView<ExampleTaskViewModel>
{
    private Entry _blankEntry = null;
    
    public ExampleTaskView()
    {
        InitializeComponent();

        this.WhenActivated(disposables =>
        {
            this.WhenAnyValue(
                    view => view.ViewModel.BeforeBlankText,
                    view => view.ViewModel.AfterBlankText)
                .Where(texts => !string.IsNullOrEmpty(texts.Item1) && !string.IsNullOrEmpty(texts.Item2))
                .Subscribe(x => UpdateLayout())
                .DisposeWith(disposables);
            
            this.WhenAnyValue(view => view.ViewModel.Status)
                .Subscribe(x => UpdateReadonlyState())
                .DisposeWith(disposables);
        });
    }

    private void UpdateLayout()
    {
        _layout.Children.Clear();

        Label[] beforeBlankWordLabels = CreateWordLabels(ViewModel.BeforeBlankText);
        foreach (Label wordLabel in beforeBlankWordLabels)
        {
            _layout.Children.Add(wordLabel);
        }
        
        _blankEntry = CreateBlankEntry();
        _blankEntry.SetBinding(Entry.TextProperty, 
            new Binding(nameof(ExampleTaskViewModel.InputBlankText), BindingMode.TwoWay, source: ViewModel));
        _layout.Children.Add(_blankEntry);

        Label[] afterBlankWordLabels = CreateWordLabels(ViewModel.AfterBlankText);
        foreach (Label wordLabel in afterBlankWordLabels)
        {
            _layout.Children.Add(wordLabel);
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
            LineBreakMode = LineBreakMode.NoWrap,
            VerticalOptions = LayoutOptions.Center
        };
    }

    private Entry CreateBlankEntry()
    {
        return new Entry()
        {
            WidthRequest = 90d,
            VerticalOptions = LayoutOptions.Center,
            IsReadOnly = CheckingStatusToReadonly()
        };
    }
    
    private void UpdateReadonlyState()
    {
        if (_blankEntry != null)
        {
            _blankEntry.IsReadOnly = CheckingStatusToReadonly();
        }
    }

    private bool CheckingStatusToReadonly()
    {
        switch (ViewModel.Status)
        {
            case CheckingStatus.Unchecked:
            default:
                return false;

            case CheckingStatus.Correct:
            case CheckingStatus.Incorrect:
                return true;
        }
    }
}