using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace WpfPerfBench.Views
{
    /// <summary>
    /// Interaction logic for InitView.xaml
    /// </summary>
    public partial class InitView : UserControl
    {
        public InitView()
        {
            InitializeComponent();
        }

        // NextCommand
        public static readonly DependencyProperty NextCommandProperty = DependencyProperty.Register(
            nameof(NextCommand), typeof(ICommand), typeof(InitView), new PropertyMetadata(default(ICommand)));

        public ICommand NextCommand
        {
            get => (ICommand)GetValue(NextCommandProperty);
            set => SetValue(NextCommandProperty, value);
        }
    }
}
