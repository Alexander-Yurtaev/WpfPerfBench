using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for Footer.xaml
    /// </summary>
    public partial class Footer : UserControl
    {
        public Footer()
        {
            InitializeComponent();
        }

        // Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(Footer), new PropertyMetadata("Пояснение"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }
    }
}
