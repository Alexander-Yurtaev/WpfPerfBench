using System.Windows;
using System.Windows.Controls;

namespace WpfPerfBench.Controls
{
    /// <summary>
    /// Interaction logic for InitProgressStand.xaml
    /// </summary>
    public partial class InitProgressStand : UserControl
    {
        public InitProgressStand()
        {
            InitializeComponent();
        }

        // ProgressLabel
        public static readonly DependencyProperty ProgressLabelProperty = DependencyProperty.Register(
            nameof(ProgressLabel), typeof(string), typeof(InitProgressStand), new PropertyMetadata("🔍 Проверка подключения..."));

        public string ProgressLabel
        {
            get => (string)GetValue(ProgressLabelProperty);
            set => SetValue(ProgressLabelProperty, value);
        }

        // ProgressStatus
        public static readonly DependencyProperty ProgressStatusProperty = DependencyProperty.Register(
            nameof(ProgressStatus), typeof(string), typeof(InitProgressStand), new PropertyMetadata("✅ Инициализация завершена! БД готова к работе."));

        public string ProgressStatus
        {
            get => (string)GetValue(ProgressStatusProperty);
            set => SetValue(ProgressStatusProperty, value);
        }

        // TotalRecords
        public static readonly DependencyProperty TotalRecordsProperty = DependencyProperty.Register(
            nameof(TotalRecords), typeof(int), typeof(InitProgressStand), new PropertyMetadata(0));

        public int TotalRecords
        {
            get { return (int)GetValue(TotalRecordsProperty); }
            set { SetValue(TotalRecordsProperty, value); }
        }

        // ProgressMessage
        public static readonly DependencyProperty ProgressMessageProperty = DependencyProperty.Register(
            nameof(ProgressMessage), typeof(string), typeof(InitProgressStand), new PropertyMetadata("📌 Данные отсутствуют ⚡ Рекомендуется наполнить тестовыми данными"));

        public string ProgressMessage
        {
            get => (string)GetValue(ProgressMessageProperty);
            set => SetValue(ProgressMessageProperty, value);
        }
    }
}
