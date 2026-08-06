namespace WpfPerfBench.WPF.ViewModels.Dialogs;

public class WarningDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public WarningDialog(string description) : base(
        "pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/warning.png",
        "Внимание!", 
        description)
    {
    }

    #endregion
}