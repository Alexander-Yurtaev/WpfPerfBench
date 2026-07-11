using System.Windows;
using System.Windows.Input;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }

    private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }
}