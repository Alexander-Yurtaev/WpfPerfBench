using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Data;
using WpfPerfBench.Data.DataContexts;
using WpfPerfBench.Data.Services;
using WpfPerfBench.Enums;
using WpfPerfBench.Services;
using WpfPerfBench.Wrappers;

namespace WpfPerfBench.SeedMethods;

public abstract partial class SeedMethodBase : ObservableObject
{
    protected readonly IDataService DataService;
    protected readonly IGeneratorService GeneratorService;
    protected readonly IMessageService MessageService;
    protected CancellationTokenSource? TokenSource;

    protected SeedMethodBase(
        IDataService dataService,
        IGeneratorService generatorService,
        IMessageService messageService)
    {
        DataService = dataService;
        GeneratorService = generatorService;
        MessageService = messageService;

        var model = new SeedMethodStat();
        Stat = new SeedMethodStatWrapper(model);
    }

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private MethodMetrics _methodMetrics = new MethodMetrics();

    [ObservableProperty]
    private SeedStatus _status = SeedStatus.None;

    public SeedMethodStatWrapper Stat { get; set; }

    public void Init(int totalItemCount)
    {
        var model = new SeedMethodStat { TotalItemCount = totalItemCount };
        Stat = new SeedMethodStatWrapper(model);
        OnPropertyChanged(nameof(Stat));
    }

    [RelayCommand]
    private async Task Seed()
    {
        if (TokenSource != null)
        {
            await TokenSource.CancelAsync();
        }

        TokenSource = new CancellationTokenSource();
        CancelCommand.NotifyCanExecuteChanged();
        var token = TokenSource.Token;

        var db = DataService.CreateContext();
        var prepared = await Prepare(db, token);
        if (!prepared) return;
        await OnSeed(db, token);
    }

    [RelayCommand(CanExecute = nameof(CanCancel))]
    private async Task Cancel()
    {
        if (TokenSource is null) return;
        await TokenSource.CancelAsync();
    }

    private bool CanCancel => TokenSource is not null;

    protected abstract Task<bool> Prepare(IWpfPerfBenchContext db, CancellationToken ct);

    protected abstract Task OnSeed(IWpfPerfBenchContext db, CancellationToken ct);
}