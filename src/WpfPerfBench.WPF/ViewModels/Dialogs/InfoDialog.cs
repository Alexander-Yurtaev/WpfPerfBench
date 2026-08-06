namespace WpfPerfBench.WPF.ViewModels.Dialogs;

public class InfoDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public InfoDialog(string header, string description) : base(
        "pack://application:,,,/WpfPerfBench.WPF;component/Resources/Icons/info.png",
        header, description)
    {
    }

    #endregion
}