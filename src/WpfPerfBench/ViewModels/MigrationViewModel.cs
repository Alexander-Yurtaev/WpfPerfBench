using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfPerfBench.Core.Enum;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.ViewModels;

public partial class MigrationViewModel : ViewModelBase, IMigrationViewModel, ILoadable
{
    private readonly IDataService _dataService;
    private readonly IMessageService _messageService;

    [ObservableProperty]
    private ObservableCollection<MigrationItem> _items = [];

    public MigrationViewModel(IDataService dataService, IMessageService messageService)
    {
        _dataService = dataService;
        _messageService = messageService;
    }

    #region Implementation of ILoadable

    public async Task LoadAsync(CancellationToken ct)
    {
        try
        {
            Items.Clear();
            var appliedMessage = await _dataService.GetAppliedMigrationsAsync(null!, ct);
            ProcessMigrationResult(appliedMessage, MigrationStatus.Applied);
            var pendingMessage = await _dataService.GetPendingMigrationsAsync(null!, ct);
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
                }

                break;
            }
            default:
                throw new InvalidOperationException($"Неизвестный тип результата: {result.GetType()}");
        }
    }

    #endregion Private Methods
}