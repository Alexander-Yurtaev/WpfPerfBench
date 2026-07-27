using WpfPerfBench.Enums;
using WpfPerfBench.ViewModels.Dialogs;

namespace WpfPerfBench.Services;

public interface IMessageService
{
    void ShowErrorMessage(string message);
    void ShowInfoMessage(string header, string message);
    void ShowSuccessMessage(string header, string message);
    void ShowWarningMessage(string message);

    void AddDialogFactory(MessageType type, Func<object[], BaseDialog> factory);
}