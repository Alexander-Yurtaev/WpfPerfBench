using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    public partial class Header : UserControl
    {
        public Header()
        {
            InitializeComponent();
        }

        // HeaderIcon
        public static readonly DependencyProperty HeaderIconProperty = DependencyProperty.Register(
            nameof(HeaderIcon), typeof(string), typeof(Header), new PropertyMetadata("🔘"));

        public string HeaderIcon
        {
            get => (string)GetValue(HeaderIconProperty);
            set => SetValue(HeaderIconProperty, value);
        }

        // Title
        public static readonly DependencyProperty HeaderTitleProperty = DependencyProperty.Register(
            nameof(HeaderTitle), typeof(string), typeof(Header), new PropertyMetadata("Заголовок"));

        public string HeaderTitle
        {
            get => (string)GetValue(HeaderTitleProperty);
            set => SetValue(HeaderTitleProperty, value);
        }

        // CurrentStep
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register(
            nameof(CurrentStep), typeof(int), typeof(Header), new PropertyMetadata(0));

        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        // TotalSteps
        public static readonly DependencyProperty TotalStepsProperty = DependencyProperty.Register(
            nameof(TotalSteps), typeof(int), typeof(Header), new PropertyMetadata(0));

        public int TotalSteps
        {
            get => (int)GetValue(TotalStepsProperty);
            set => SetValue(TotalStepsProperty, value);
        }
    }
}
