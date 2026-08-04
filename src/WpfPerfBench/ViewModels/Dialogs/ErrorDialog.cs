namespace WpfPerfBench.ViewModels.Dialogs;

public partial class ErrorDialog : BaseDialog
{
    public ErrorDialog(string description) : base(
        "pack://application:,,,/WpfPerfBench;component/Resources/Icons/cross_mark.png",
        "Критическая ошибка", description)
    {
        
    }
}