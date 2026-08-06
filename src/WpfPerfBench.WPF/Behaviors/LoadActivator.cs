using System.Windows;
using WpfPerfBench.Core.Interfaces;

namespace WpfPerfBench.WPF.Behaviors;

public static class LoadActivator
{
    #region IsActive

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
        "IsActive", 
        typeof(bool), 
        typeof(LoadActivator), 
        new PropertyMetadata(false, IsActiveChangedCallback));

    public static bool GetIsActive(DependencyObject element)
    {
        return (bool)element.GetValue(IsActiveProperty);
    }

    public static void SetIsActive(DependencyObject element, bool value)
    {
        element.SetValue(IsActiveProperty, value);
    }

    private static void IsActiveChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            if (e.NewValue is not bool flag) return;

            if (d is not FrameworkElement element) return;

            if (!flag)
            {
                element.Loaded -= ElementOnLoaded;
                element.DataContextChanged -= ElementOnDataContextChanged;
                return;
            }
            
            if (element is { IsLoaded: true, DataContext: not null })
            {
                var _ = LoadViewModel(element);
            }
            else
            {
                element.Loaded += ElementOnLoaded;
                element.DataContextChanged += ElementOnDataContextChanged;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static async void ElementOnLoaded(object sender, RoutedEventArgs e)
    {
        try
        {
            await CheckAndLoadViewModel(sender);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    private static async void ElementOnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        try
        {
            await CheckAndLoadViewModel(sender);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
        }
    }

    #endregion IsActive

    #region CancellationTokenSource

    public static readonly DependencyProperty CancellationTokenSourceProperty = DependencyProperty.RegisterAttached(
        "CancellationTokenSource", 
        typeof(CancellationTokenSource), 
        typeof(LoadActivator), 
        new PropertyMetadata(default(CancellationTokenSource)));

    public static CancellationTokenSource GetCancellationTokenSource(DependencyObject element)
    {
        return (CancellationTokenSource)element.GetValue(CancellationTokenSourceProperty);
    }

    public static void SetCancellationTokenSource(DependencyObject element, CancellationTokenSource value)
    {
        element.SetValue(CancellationTokenSourceProperty, value);
    }

    #endregion CancellationTokenSource

    #region Private Methods

    private static async Task CheckAndLoadViewModel(object sender)
    {
        try
        {
            if (sender is not FrameworkElement element) return;
            if (element is { IsLoaded: true, DataContext: not null })
            {
                await LoadViewModel(element);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

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