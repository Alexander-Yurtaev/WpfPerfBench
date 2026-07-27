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
        messageService.AddDialogFactory(MessageType.Info, CreateInfoDialog);
        messageService.AddDialogFactory(MessageType.Success, CreateSuccessDialog);
        messageService.AddDialogFactory(MessageType.Warning, CreateWarningDialog);
    }

    #region Private Methods

    private static BaseDialog CreateErrorDialog(object[] args)
    {
        if (args is null) throw new ArgumentNullException();
        if (args.Length == 0) throw new ArgumentException();
        var message = args[0]?.ToString() ?? "";
        return new ErrorDialog(message);
    }

    private static BaseDialog CreateInfoDialog(object[] args)
    {
        if (args is null) throw new ArgumentNullException();
        if (args.Length < 2) throw new ArgumentException();
        var header = args[0]?.ToString() ?? "";
        var description = args[1]?.ToString() ?? "";
        return new InfoDialog(header, description);
    }

    private static BaseDialog CreateSuccessDialog(object[] args)
    {
        if (args is null) throw new ArgumentNullException();
        if (args.Length < 2) throw new ArgumentException();
        var header = args[0]?.ToString() ?? "";
        var description = args[1]?.ToString() ?? "";
        return new SuccessDialog(header, description);
    }

    private static BaseDialog CreateWarningDialog(object[] args)
    {
        if (args is null) throw new ArgumentNullException();
        if (args.Length == 0) throw new ArgumentException();
        var description = args[0]?.ToString() ?? "";
        return new WarningDialog(description);
    }

    #endregion Private Methods
}