using Microsoft.Xaml.Behaviors;
using System.Windows;
using WpfPerfBench.Interfaces;

namespace WpfPerfBench.Behaviors;

public class LoadBehavior : Behavior<FrameworkElement>
{
    private CancellationTokenSource? _cts;

    #region Overrides of Behavior

    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.Loaded += AssociatedObjectOnLoaded;
        AssociatedObject.DataContextChanged += AssociatedObjectOnDataContextChanged;
    }

    protected override void OnDetaching()
    {
        base.OnDetaching();
        AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
        AssociatedObject.DataContextChanged -= AssociatedObjectOnDataContextChanged;
    }

    #endregion

    #region Private Methods

    private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs e)
    {
        var _ = LoadViewModel();
    }

    private void AssociatedObjectOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        var _ = LoadViewModel();
    }

    private async Task LoadViewModel()
    {
        if (_cts is not null)
        {
            await _cts.CancelAsync();
        }

        _cts = new CancellationTokenSource();
        var ct = _cts.Token;

        if (AssociatedObject.DataContext is ILoadable vm)
        {
            await vm.LoadAsync(ct);
        }
    }

    #endregion Private Methods
}