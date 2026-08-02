using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;
using GMap.NET.WindowsPresentation;

namespace WpfPerfBench.Behaviors;

public class DragMoveBehavior : Behavior<Window>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        AssociatedObject.MouseLeftButtonDown += AssociatedObjectOnMouseLeftButtonDown;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseLeftButtonDown -= AssociatedObjectOnMouseLeftButtonDown;
        base.OnDetaching();
    }

    private void AssociatedObjectOnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        if (e.ClickCount != 1) return;
        if (e.OriginalSource is GMapControl) return;
        AssociatedObject.DragMove();
    }
}