using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for Pager.xaml
    /// </summary>
    public partial class Pager : UserControl
    {
        public Pager()
        {
            InitializeComponent();
        }

        // CurrentStep
        public static readonly DependencyProperty CurrentStepProperty = DependencyProperty.Register(
            nameof(CurrentStep), typeof(int), typeof(Pager), new PropertyMetadata(0));

        public int CurrentStep
        {
            get => (int)GetValue(CurrentStepProperty);
            set => SetValue(CurrentStepProperty, value);
        }

        // TotalSteps
        public static readonly DependencyProperty TotalStepsProperty = DependencyProperty.Register(
            nameof(TotalSteps), typeof(int), typeof(Pager), new PropertyMetadata(0));

        public int TotalSteps
        {
            get => (int)GetValue(TotalStepsProperty);
            set => SetValue(TotalStepsProperty, value);
        }
    }
}
