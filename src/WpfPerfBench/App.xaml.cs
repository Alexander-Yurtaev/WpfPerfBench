using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Repositories;

namespace WpfPerfBench;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public IServiceProvider ServiceProvider { get; private set; } = null!;

    public App()
    {
        this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException;

    }

    private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
    {
        MessageBox.Show($"Error: {e.Exception.Message}\n{e.Exception.StackTrace}",
            "Critical Error",
            MessageBoxButton.OK,
            MessageBoxImage.Error);
        e.Handled = false;
    }

    private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
    {
        var exception = e.ExceptionObject as Exception;
        MessageBox.Show($"Unhandled exception: {exception?.Message}\n{exception?.StackTrace}");
    }

    private void TaskScheduler_UnobservedTaskException(object? sender, UnobservedTaskExceptionEventArgs e)
    {
        MessageBox.Show($"Error in Task: {e.Exception.Message}\n{e.Exception.StackTrace}");
        e.SetObserved();
    }

    #region Overrides of Application

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        // 
        services.AddSingleton<IUserSession>(new UserSession());
        services.AddSingleton<IDataContextFactory, DataContextFactory>();
        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(App).Assembly));

        services.AddTransient<ICategoryRepository, CategoryRepository>();

        ServiceProvider = services.BuildServiceProvider();
    }

    #endregion
}

