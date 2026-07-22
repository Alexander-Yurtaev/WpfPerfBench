using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Interfaces.Services;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Repositories;
using WpfPerfBench.Interfaces;
using WpfPerfBench.Interfaces.ViewModels;

namespace WpfPerfBench.ViewModels;

public partial class StandViewModel : ViewModelBase, IStandViewModel, ILoadable
{
    private readonly ICategoryRepository _categoryRepository;

    [ObservableProperty]
    private string _icon;

    [ObservableProperty]
    private string _fio;

    [ObservableProperty]
    private string _dataProvider;

    [ObservableProperty]
    private string _themeIcon;

    [ObservableProperty]
    private int _totalRecordCount;

    [ObservableProperty]
    private StatItem[] _statItems;

    [ObservableProperty]
    private StatItem[] _treeItems;

    public StandViewModel(
        INavigationService navigationService,
        IUserSession userSession, 
        ICategoryRepository categoryRepository) : base(navigationService, userSession)
    {
        _categoryRepository = categoryRepository;
        
        Header = new Controls.HeaderViewModel("Рабочий стенд");

        Icon = "👋";
        Fio = userSession.Fio;
        this.DataProvider = userSession.DataProvider.ToString();
        ThemeIcon = "☀️";
        StatItems = [];
    }

    #region Implementation of ILoadable

    public async Task LoadAsync(CancellationToken ct)
    {
        await Task.CompletedTask;
        return;
        //var categoryTree = await _categoryRepository.HierarchyCategories(ct);

        //TotalRecordCount = categoryTree.Count;
    }

    #endregion
}