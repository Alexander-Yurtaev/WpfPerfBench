using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Models;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;
using WpfPerfBench.Managers;
using WpfPerfBench.Services;

namespace WpfPerfBench.ViewModels;

public partial class StandViewModel : ViewModelBase, IStandViewModel, ILoadableAsync
{
    private readonly IBusyManager _busyManager;
    private readonly IMessageService _messageService;
    private readonly IDataService _dataService;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private string _fio;

    [ObservableProperty]
    private string _dataProvider;

    [ObservableProperty]
    private int _totalRecordCount;

    [ObservableProperty]
    private StatItem[] _statItems;

    public ObservableCollection<CategoryTreeItem> TreeItems { get; set; }

    public StandViewModel(
        IBusyManager busyManager,
        IMessageService messageService,
        INavigationService navigationService,
        IUserSession userSession,
        IDataService dataService) : base(navigationService, userSession)
    {
        _busyManager = busyManager;
        _messageService = messageService;
        _dataService = dataService;

        Icon = "👋";
        Fio = userSession.Fio;
        DataProvider = userSession.DataProvider.ToString();
        TotalRecordCount = 1000;
        StatItems = [];
        TreeItems = [];
    }

    #region Implementation of ILoadableAsync

    public async Task LoadAsync(CancellationToken ct)
    {
        var ctLocal = _busyManager.ShowStandardIndicator("Загрузка...");
        var ctTotal = CancellationTokenSource.CreateLinkedTokenSource(ct, ctLocal);

        try
        {
            var result = await _dataService.HierarchyCategories(ctTotal.Token);

            TreeItems.Clear();

            switch (result)
            {
                case FailResult failResult:
                    _messageService.ShowErrorMessage(failResult.Message);
                    break;
                case EntityResult<CategoryTreeItem> entities:
                    {
                        TotalRecordCount = 0;
                        foreach (var category in entities.Entities)
                        {
                            ctLocal.ThrowIfCancellationRequested();
                            TotalRecordCount += CalcTotalRecordCount(category);
                            TreeItems.Add(category);
                        }

                        break;
                    }
            }
        }
        catch (TaskCanceledException)
        {
            _messageService.ShowWarningMessage("Операция отменена");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            _messageService.ShowErrorMessage(e.Message);
        }
        finally
        {
            _busyManager.CloseIndicator();
        }
    }

    #endregion Implementation of ILoadableAsync

    private int CalcTotalRecordCount(CategoryTreeItem item)
    {
        return item.ItemsCount + item.Children.Sum(CalcTotalRecordCount);
    }
}