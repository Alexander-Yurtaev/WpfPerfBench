using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WpfPerfBench.Core.Interfaces;
using WpfPerfBench.Core.Managers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Extensions;
using WpfPerfBench.Factories;
using WpfPerfBench.Interfaces.Managers;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels;
using WpfPerfBench.Views;

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
        services.AddLogging();

        services.AddSingleton<IUserSession, UserSession>();
        services.AddMessageService();
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddScoped<IBusyManager, BusyManager>();

        services.AddTransient<IThemeManager, ThemeManager>();
        services.AddSingleton<IConsoleManager, ConsoleManager>();

        services.AddTransient<IDataService, DataService>();
        services.AddTransient<IGeneratorService, GeneratorService>();
        services.AddTransient<IDataContextFactory, DataContextFactory>();
        services.AddTransient<ISeedMethodFactory, SeedMethodFactory>();

        services.AddAutoMapper(cfg => cfg.AddMaps(typeof(App).Assembly));

        services.AddTransient<ICategoryRepository, CategoryRepository>();
        services.AddTransient<IItemRepository, ItemRepository>();

        services.AddSingleton<IInitViewModel, InitViewModel>();
        services.AddTransient<Func<IInitViewModel>>(sp => sp.GetRequiredService<IInitViewModel>);

        services.AddSingleton<IMigrationViewModel, MigrationViewModel>();
        services.AddTransient<Func<IMigrationViewModel>>(sp => sp.GetRequiredService<IMigrationViewModel>);

        services.AddSingleton<ISeedViewModel, SeedViewModel>();
        services.AddTransient<Func<ISeedViewModel>>(sp => sp.GetRequiredService<ISeedViewModel>);

        services.AddSingleton<IStandViewModel, StandViewModel>();
        services.AddTransient<Func<IStandViewModel>>(sp => sp.GetRequiredService<IStandViewModel>);

        services.AddTransient<MainWindowViewModel>();
        services.AddTransient<MainWindow>();

        ServiceProvider = services.BuildServiceProvider();

        ServiceProvider.UseMessageService();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
    }

    #endregion
}

