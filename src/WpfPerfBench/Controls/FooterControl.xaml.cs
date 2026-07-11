using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for FooterControl.xaml
    /// </summary>
    public partial class FooterControl : UserControl
    {
        public FooterControl()
        {
            InitializeComponent();
        }

        // Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(FooterControl), new PropertyMetadata("Пояснение"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}
