using System.Windows;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Commands;
using WpfPerfBench.Enums;
using WpfPerfBench.ViewModels.Dialogs;
using WpfPerfBench.Views;

namespace WpfPerfBench.Services;

public class MessageService : IMessageService
{
    private readonly Func<ModalDialog> _dialogFactory;
    private readonly Dictionary<MessageType, Func<object[], BaseDialog>> _dialogFactories = [];

    public MessageService(Func<ModalDialog> dialogFactory)
    {
        _dialogFactory = dialogFactory;
    }

    #region Implementation of IMessageService

    public void ShowErrorMessage(string message)
    {
        ShowDialog([message], MessageType.Error);
    }

    public void ShowInfoMessage(string header, string message)
    {
        ShowDialog([header, message], MessageType.Info);
    }

    public void ShowSuccessMessage(string header, string message)
    {
        ShowDialog([header, message], MessageType.Success);
    }

    public void ShowWarningMessage(string message)
    {
        ShowDialog([message], MessageType.Warning);
    }

    #endregion

    public void AddDialogFactory(MessageType type, Func<object[], BaseDialog> factory)
    {
        _dialogFactories[type] = factory;
    }

    private void ShowDialog(object[] args, MessageType type)
    {
        if (_dialogFactories.TryGetValue(type, out var factory))
        {
            var dialog = _dialogFactory();
            var dialogVm = factory(args);
            var command = new RelayUICommand("Закрыть", CreateCloseCommand(dialog));
            dialogVm.PrimaryCommand = command;
            dialog.DataContext = dialogVm;
            dialog.Owner = Application.Current.MainWindow;
            dialog.ShowDialog();
        }
        else
        {
            throw new InvalidOperationException($"Необходимо зарегистрировать диалог: {MessageType.Error}Dialog");
        }
    }

    public RelayCommand CreateCloseCommand(ModalDialog dialog)
    {
        var command = new RelayCommand(dialog.Close);
        return command;
    }
}