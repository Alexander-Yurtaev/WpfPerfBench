using System.Windows;

namespace WpfPerfBench.WPF.Behaviors;

public static class FocusOnLoad
{
    #region IsActive

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
        "IsActive", 
        typeof(bool), 
        typeof(FocusOnLoad), 
        new PropertyMetadata(false, PropertyChangedCallback));

    public static void SetIsActive(DependencyObject element, bool value)
    {
        element.SetValue(IsActiveProperty, value);
    }

    public static bool GetIsActive(DependencyObject element)
    {
        return (bool)element.GetValue(IsActiveProperty);
    }

    private static void PropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            if (e.NewValue is not (bool and true)) return;
            if (d is not FrameworkElement element) return;
            if (element is { IsLoaded: true })
            {
                element.Focus();
            }
            else
            {
                element.Loaded += ElementOnLoaded;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static void ElementOnLoaded(object sender, RoutedEventArgs e)
    {
        Focus(sender);
    }

    #endregion IsActive

    #region Private Methods

    private static void Focus(object sender)
    {
        try
        {
            if (sender is not FrameworkElement element) return;
            if (element is { IsLoaded: true })
            {
                element.Focus();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    #endregion Private Methods
}