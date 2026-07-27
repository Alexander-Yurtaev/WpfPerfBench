namespace WpfPerfBench.ViewModels.Dialogs;

public partial class WarningDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public WarningDialog(string description) : base(
        "pack://application:,,,/Resources/Icons/warning.png",
        "Внимание!", 
        description)
    {
    }

    #endregion
}