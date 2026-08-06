namespace WpfPerfBench.WPF.ViewModels.Dialogs;

public class ErrorDialog : BaseDialog
{
    public ErrorDialog(string description) : base(
        "pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/cross_mark.png",
        "Критическая ошибка", description)
    {
        
    }
}