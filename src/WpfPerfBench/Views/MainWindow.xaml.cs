using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private const uint DWMWA_WINDOW_CORNER_PREFERENCE = 33;
    private const int DWMWCP_ROUND = 2;

    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();

        this.SourceInitialized += (_, _) =>
        {
            var helper = new WindowInteropHelper(this);
            int pref = DWMWCP_ROUND;
            _ = DwmSetWindowAttribute(helper.Handle, DWMWA_WINDOW_CORNER_PREFERENCE, ref pref, sizeof(int));
        };

        DataContext = viewModel;
    }

    private void CloseCommandHandler(object sender, ExecutedRoutedEventArgs e)
    {
        this.Close();
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr hwnd, uint attr, ref int attrValue, uint attrSize);
}