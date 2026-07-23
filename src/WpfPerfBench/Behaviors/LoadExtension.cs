using Microsoft.Xaml.Behaviors;
using System.Windows;
using WpfPerfBench.Interfaces;

namespace WpfPerfBench.Behaviors;

public static class LoadExtension
{
    #region IsActive

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
        "IsActive", typeof(bool), typeof(LoadExtension), new PropertyMetadata(false, PropertyChangedCallback));

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
            if (element is { DataContext: not null })
            {
                var _ = LoadViewModel(element);
            }
            else
            {
                element.DataContextChanged += ElementOnDataContextChanged;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static void ElementOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            if (sender is not FrameworkElement element) return;
            if (element is { DataContext: not null })
            {
                var _ = LoadViewModel(element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    #endregion IsActive

    #region CancellationTokenSource

    public static readonly DependencyProperty CancellationTokenSourceProperty = DependencyProperty.RegisterAttached(
        "CancellationTokenSource", typeof(CancellationTokenSource), typeof(LoadExtension), new PropertyMetadata(default(CancellationTokenSource)));

    public static void SetCancellationTokenSource(DependencyObject element, CancellationTokenSource value)
    {
        element.SetValue(CancellationTokenSourceProperty, value);
    }

    public static CancellationTokenSource GetCancellationTokenSource(DependencyObject element)
    {
        return (CancellationTokenSource)element.GetValue(CancellationTokenSourceProperty);
    }

    #endregion CancellationTokenSource

    #region Private Methods

    private static async Task LoadViewModel(FrameworkElement sender)
    {
        var cts = (CancellationTokenSource)sender.GetValue(CancellationTokenSourceProperty);

        switch (sender.DataContext)
        {
            case ILoadableAsync lda:
            {
                if (cts is not null)
                {
                    await cts.CancelAsync();
                }

                cts = new CancellationTokenSource();

                await lda.LoadAsync(cts.Token);
                break;
            }
            case ILoadable ld:
                ld.Load();
                break;
        }
    }

    #endregion Private Methods
}