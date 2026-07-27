using WpfPerfBench.Enums;
using WpfPerfBench.ViewModels.Dialogs;

namespace WpfPerfBench.Services;

public interface IMessageService
{
    void ShowErrorMessage(string message);
    void AddDialogFactory(MessageType type, Func<object[], BaseDialog> factory);
}