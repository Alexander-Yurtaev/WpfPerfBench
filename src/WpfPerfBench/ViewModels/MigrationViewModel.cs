using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Enums;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.Services;
using WpfPerfBench.Wrappers;

namespace WpfPerfBench.ViewModels;

public partial class MigrationViewModel : ViewModelBase, IMigrationViewModel, ILoadableAsync
{
    private readonly IDataService _dataService;
    private readonly IMessageService _messageService;
    private readonly IBusyManager _busyManager;

    [ObservableProperty] private ObservableCollection<MigrationItem> _items = [];

    public MigrationViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        IDataService dataService,
        IMessageService messageService,
        IBusyManager busyManager) : base(navigationService, userSession)
    {
        _dataService = dataService;
        _messageService = messageService;
        _busyManager = busyManager;
    }

    public int MigrationCount => Items.Count;

    [RelayCommand(CanExecute = nameof(CanApplyMigrations))]
    private async Task ApplyMigrations()
    {
        var ct = _busyManager.ShowStandardIndicator("Миграции...");
        try
        {
            var db = _dataService.CreateContext();
            var migrations = Items
                .Where(i => i.Status == MigrationStatus.Pending);

            var isFailed = false;
            foreach (var migration in migrations)
            {
                if (isFailed)
                {
                    migration.Status = MigrationStatus.Skipped;
                    continue;
                }

                var result = await _dataService.Migrate(db, migration.Name, ct);
                switch (result.Success)
                {
                    case true:
                        migration.Status = MigrationStatus.Applied;
                        break;
                    case false:
                        migration.Status = MigrationStatus.Failed;
                        isFailed = true;
                        _messageService.ShowErrorMessage(result.Message);
                        break;
                }
            }

            if (!isFailed)
            {
                _messageService.ShowSuccessMessage("Миграции применены!", "Вы можете продолжить работу.");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        finally
        {
            _busyManager.CloseIndicator();
        }
    }

    private bool CanApplyMigrations() => Items.Any(i => i.Status == MigrationStatus.Pending);

    #region Implementation of ILoadableAsync

    public async Task LoadAsync(CancellationToken ct)
    {
        var ctLocal = _busyManager.ShowStandardIndicator("Загрузка...");
        var ctTotal = CancellationTokenSource.CreateLinkedTokenSource(ct, ctLocal);

        Items.Clear();
        ApplyMigrationsCommand.NotifyCanExecuteChanged();
        OnPropertyChanged(nameof(MigrationCount));

        try
        {
            await Task.Run(async () =>
                {
                    var db = _dataService.CreateContext();
                    var appliedMessage = await _dataService.GetAppliedMigrationsAsync(db, ctTotal.Token);
                    ProcessMigrationResult(appliedMessage, MigrationStatus.Applied);
                    var pendingMessage = await _dataService.GetPendingMigrationsAsync(db, ctTotal.Token);
                    ProcessMigrationResult(pendingMessage, MigrationStatus.Pending);
                }, ctTotal.Token)
                .ContinueWith(task =>
                {
                    if (task.Exception is null) return;
                    throw task.Exception;
                }, ctTotal.Token);
        }
        catch (TaskCanceledException e)
        {
            Console.WriteLine(e);
            Items.Clear();
            _messageService.ShowWarningMessage("Операция загрузки миграций отменена.");
        }
        catch (AggregateException e)
        {
            Console.WriteLine(e);
            Items.Clear();
            var message = new StringBuilder();
            foreach (var exception in e.InnerExceptions)
            {
                message.Append(exception.GetBaseException().Message);
            }
            _messageService.ShowErrorMessage(message.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            Items.Clear();
            _messageService.ShowErrorMessage(e.Message);
        }
        finally
        {
            ApplyMigrationsCommand.NotifyCanExecuteChanged();
            _busyManager.CloseIndicator();
        }
    }

    #endregion

    #region Private Methods

    private void ProcessMigrationResult(ResultBase result, MigrationStatus status)
    {
        switch (result)
        {
            case FailResult fail:
                throw new InvalidOperationException(fail.Message);
            case CancelResult _:
                break;
            case EntityResult<string> names:
                {
                    foreach (var name in names.Entities)
                    {
                        if (Application.Current is not null)
                        {
                            Application.Current.Dispatcher.Invoke(() =>
                                Items.Add(new MigrationItem(name) { Status = status }));
                        }
                        else
                        {
                            Items.Add(new MigrationItem(name) { Status = status });
                        }
                            
                        OnPropertyChanged(nameof(MigrationCount));
                    }

                    break;
                }
            default:
                throw new InvalidOperationException($"Неизвестный тип результата: {result.GetType()}");
        }
    }

    #endregion Private Methods
}