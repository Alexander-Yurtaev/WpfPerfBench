using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.Services;

namespace WpfPerfBench.ViewModels;

public partial class MigrationViewModel : ViewModelBase, IMigrationViewModel, ILoadableAsync
{
    private readonly IDataService _dataService;
    private readonly IMessageService _messageService;

    [ObservableProperty] private ObservableCollection<MigrationItem> _items = [];

    public MigrationViewModel(
        INavigationService navigationService,
        IUserSession userSession,
        IDataService dataService,
        IMessageService messageService) : base(navigationService, userSession)
    {
        _dataService = dataService;
        _messageService = messageService;
    }

    public int MigrationCount => Items.Count;

    [RelayCommand(CanExecute = nameof(CanApplyMigrations))]
    private async Task ApplyMigrations()
    {
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
                var result = await _dataService.Migrate(db, migration.Name, CancellationToken.None);
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
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        _messageService.ShowSuccessMessage("Миграции применены!", "Вы можете продолжить работу.");
    }

    private bool CanApplyMigrations() => Items.Any(i => i.Status == MigrationStatus.Pending);

    #region Implementation of ILoadableAsync

    public async Task LoadAsync(CancellationToken ct)
    {
        try
        {
            Items.Clear();
            OnPropertyChanged(nameof(MigrationCount));
            var db = _dataService.CreateContext();
            var appliedMessage = await _dataService.GetAppliedMigrationsAsync(db, ct);
            ProcessMigrationResult(appliedMessage, MigrationStatus.Applied);
            var pendingMessage = await _dataService.GetPendingMigrationsAsync(db, ct);
            ProcessMigrationResult(pendingMessage, MigrationStatus.Pending);
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine(e);
            Items.Clear();
            _messageService.ShowErrorMessage(e.Message);
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
            case NamesResult names:
                {
                    foreach (var name in names.Names)
                    {
                        Items.Add(new MigrationItem(name) { Status = status });
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