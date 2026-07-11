using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for HeaderControl.xaml
    /// </summary>
    public partial class HeaderControl : UserControl
    {
        public HeaderControl()
        {
            InitializeComponent();
        }

        // Icon
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            nameof(Icon), typeof(string), typeof(HeaderControl), new PropertyMetadata("🔘"));

        public string Icon
        {
            get => (string)GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        // Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(HeaderControl), new PropertyMetadata("Заголовок"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        // CurrentStep
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register(
            nameof(CurrentStep), typeof(int), typeof(HeaderControl), new PropertyMetadata(0));

        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        // TotalSteps
        public static readonly DependencyProperty TotalStepsProperty = DependencyProperty.Register(
            nameof(TotalSteps), typeof(int), typeof(HeaderControl), new PropertyMetadata(0));

        public int TotalSteps
        {
            get => (int)GetValue(TotalStepsProperty);
            set => SetValue(TotalStepsProperty, value);
        }
    }
}
