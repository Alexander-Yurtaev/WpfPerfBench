using System.Windows;
using System.Windows.Controls;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.Behaviors;

public static class TreeViewHelper
{
    #region IsActive

    public static readonly DependencyProperty IsActiveProperty = DependencyProperty.RegisterAttached(
        "IsActive",
        typeof(bool),
        typeof(TreeViewHelper),
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
            if (e.NewValue is not (bool and true)) return;
            if (d is not TreeView treeView) return;
            treeView.SelectedItemChanged += TreeViewOnSelectedItemChanged;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    private static void TreeViewOnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
    {
        if (sender is not TreeView treeView) return;
        if (treeView.DataContext is not ITreeViewHelper helper) return;
        helper.SelectedTreeItem = e.NewValue as CategoryTreeItem;
    }

    #endregion IsActive
}