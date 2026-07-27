namespace WpfPerfBench.ViewModels.Dialogs;

public partial class InfoDialog : BaseDialog
{
    #region Overrides of BaseDialog

    public InfoDialog(string header, string description) : base(
        "pack://application:,,,/Resources/Icons/info.png",
        header, description)
    {
    }

    #endregion
}