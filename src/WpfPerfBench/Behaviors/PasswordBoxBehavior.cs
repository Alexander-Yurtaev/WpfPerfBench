using System.Security;
using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Behaviors;

public static class PasswordBoxBehavior
{
    #region PropertyName

    public static readonly DependencyProperty PropertyNameProperty = DependencyProperty.Register(
        "PropertyName", 
        typeof(string), 
        typeof(PasswordBox), 
        new PropertyMetadata(null, PropertyNameOnChanged));

    private static void PropertyNameOnChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is not PasswordBox passwordBox) return;
        passwordBox.PasswordChanged += ElementOnPasswordChanged;
    }

    public static string GetPropertyName(DependencyObject element)
    {
        return (string)element.GetValue(PropertyNameProperty);
    }

    public static void SetPropertyName(DependencyObject element, string value)
    {
        element.SetValue(PropertyNameProperty, value);
    }

    #endregion PropertyName

    private static void ElementOnPasswordChanged(object sender, RoutedEventArgs e)
    {
        if (sender is not PasswordBox passwordBox) return;
        
        var propertyName = GetPropertyName(passwordBox);
        var prop = passwordBox.DataContext.GetType().GetProperty(propertyName);

        if (prop is null) return;

        var oldSecurePassword = (SecureString?)prop.GetValue(passwordBox.DataContext);
        if (oldSecurePassword is not null)
        {
            oldSecurePassword.Clear();
            oldSecurePassword.Dispose();
        }

        var securePassword = passwordBox.SecurePassword.Copy();
        prop.SetValue(passwordBox.DataContext, securePassword);
    }
}