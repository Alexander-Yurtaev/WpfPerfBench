using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.Data.Services;
using WpfPerfBench.WPF.Interfaces.Managers;
using WpfPerfBench.WPF.Services;
using WpfPerfBench.WPF.Wrappers;

namespace WpfPerfBench.WPF.SeedMethods;

public abstract partial class SeedMethodBase : ObservableObject
{
    protected readonly IDataService DataService;
    protected readonly IGeneratorService GeneratorService;
    protected readonly IMessageService MessageService;
    private readonly INavigationService _navigationService;
    protected CancellationTokenSource? TokenSource;

    protected SeedMethodBase(
        IDataService dataService,
        IGeneratorService generatorService,
        IMessageService messageService,
        INavigationService navigationService)
    {
        DataService = dataService;
        GeneratorService = generatorService;
        MessageService = messageService;
        _navigationService = navigationService;

        var metrics = new SeedMethodMetrics();
        MethodMetrics = new SeedMethodMetricsWrapper(metrics);
    }

    [ObservableProperty]
    private string _title = string.Empty;

    [ObservableProperty]
    private string _description = string.Empty;

    [ObservableProperty]
    private SeedStatus _status = SeedStatus.None;

    public SeedMethodMetricsWrapper MethodMetrics { get; set; }

    [RelayCommand]
    private async Task Seed()
    {
        _navigationService.Block();

        try
        {
            MethodMetrics.Clean();

            if (TokenSource != null)
            {
                await TokenSource.CancelAsync();
            }

            TokenSource = new CancellationTokenSource();
            CancelCommand.NotifyCanExecuteChanged();

            this.Status = SeedStatus.Processing;

            MethodMetrics.UpdateMemoryBefore(true);
            MethodMetrics.Start();

            var result = await Seed(MethodMetrics, TokenSource.Token);

            MethodMetrics.Stop();
            MethodMetrics.UpdateMemoryAfter(true);

            switch (result)
            {
                case SuccessResult _:
                    this.Status = SeedStatus.Finished;
                    MessageService.ShowSuccessMessage(this.Title, "Данные загружены.");
                    break;
                case FailResult failResult:
                    this.Status = SeedStatus.Failed;
                    MessageService.ShowErrorMessage(failResult.Message);
                    break;
                case CancelResult cancelResult:
                    this.Status = SeedStatus.Canceled;
                    MessageService.ShowWarningMessage(cancelResult.Message);
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            MessageService.ShowWarningMessage(e.GetBaseException().Message);
        }
        finally
        {
            _navigationService.UnBlock();
        }
    }

    protected abstract Task<ResultBase> Seed(ISeedMethodMetricsRefresher metrics, CancellationToken ct);

    [RelayCommand(CanExecute = nameof(CanCancel))]
    private async Task Cancel()
    {
        if (TokenSource is null) return;
        await TokenSource.CancelAsync();
    }

    private bool CanCancel => TokenSource is not null;
}