using Microsoft.Extensions.DependencyInjection;
using WpfPerfBench.Enums;
using WpfPerfBench.Services;
using WpfPerfBench.ViewModels.Dialogs;
using WpfPerfBench.Views;

namespace WpfPerfBench.Extensions;

public static class MessageServiceExtensions
{
    public static void AddMessageService(this ServiceCollection sc)
    {
        sc.AddTransient<ModalDialog>();
        sc.AddTransient<Func<ModalDialog>>(sp => sp.GetRequiredService<ModalDialog>);

        sc.AddSingleton<IMessageService, MessageService>();
    }

    public static void UseMessageService(this IServiceProvider sp)
    {
        var messageService = sp.GetRequiredService<IMessageService>();
        messageService.AddDialogFactory(MessageType.Error, CreateErrorDialog);
    }

    #region Private Methods

    private static BaseDialog CreateErrorDialog(object[] args)
    {
        if (args is null) throw new ArgumentNullException();
        if (args.Length == 0) throw new ArgumentException();
        var message = args[0]?.ToString() ?? "";
        return new ErrorDialog(message);
    }

    #endregion Private Methods
}