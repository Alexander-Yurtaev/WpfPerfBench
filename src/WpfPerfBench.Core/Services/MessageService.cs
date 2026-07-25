using WpfPerfBench.Core.Interfaces.Services;

namespace WpfPerfBench.Core.Services;

public class MessageService : IMessageService
{
    #region Implementation of IMessageService

    public void ShowErrorMessage(string message)
    {
        System.Diagnostics.Debugger.Log(0, "ERROR","===== Start ErrorMessage ====" + Environment.NewLine);
        System.Diagnostics.Debugger.Log(0, "ERROR", message + Environment.NewLine);
        System.Diagnostics.Debugger.Log(0, "ERROR", "===== End ErrorMessage ====" + Environment.NewLine);
    }

    #endregion
}