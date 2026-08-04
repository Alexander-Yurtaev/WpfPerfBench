namespace WpfPerfBench.ViewModels.Dialogs;

public partial class WarningDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public WarningDialog(string description) : base(
        "pack://application:,,,/WpfPerfBench;component/Resources/Icons/warning.png",
        "Внимание!", 
        description)
    {
    }

    #endregion
}