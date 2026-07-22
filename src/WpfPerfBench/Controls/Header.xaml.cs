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

        // IconPath
        public static readonly DependencyProperty IconPathProperty = DependencyProperty.Register(
            nameof(IconPath), typeof(string), typeof(Header), new PropertyMetadata("pack://application:,,,/Resources/Icons/wpb.ico"));

        public string IconPath
        {
            get => (string)GetValue(IconPathProperty);
            set => SetValue(IconPathProperty, value);
        }

        // Title
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register(
            nameof(Title), typeof(string), typeof(Header), new PropertyMetadata("Заголовок"));

        public string Title
        {
            get => (string)GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
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
