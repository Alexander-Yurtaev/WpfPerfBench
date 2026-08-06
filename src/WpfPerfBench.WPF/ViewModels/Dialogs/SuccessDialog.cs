namespace WpfPerfBench.WPF.ViewModels.Dialogs;

public class SuccessDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public SuccessDialog(string header, string description) : base(
        "pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/success.png",
        header, description)
    {
    }

    #endregion
}