namespace WpfPerfBench.ViewModels.Dialogs;

public partial class SuccessDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public SuccessDialog(string header, string description) : base(
        "pack://application:,,,/Resources/Icons/success.png",
        header, description)
    {
    }

    #endregion
}