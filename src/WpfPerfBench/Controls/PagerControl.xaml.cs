using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for PagerControl.xaml
    /// </summary>
    public partial class PagerControl : UserControl
    {
        public PagerControl()
        {
            InitializeComponent();
        }

        // CurrentStep
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register(
            nameof(CurrentStep), typeof(int), typeof(PagerControl), new PropertyMetadata(0));

        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        // TotalSteps
        public static readonly DependencyProperty TotalStepsProperty = DependencyProperty.Register(
            nameof(TotalSteps), typeof(int), typeof(PagerControl), new PropertyMetadata(0));

        public int TotalSteps
        {
            get => (int)GetValue(TotalStepsProperty);
            set => SetValue(TotalStepsProperty, value);
        }
    }
}
