using WpfPerfBench.Data;

namespace WpfPerfBench.ViewModels;

public interface IStandViewModel : IViewModelBase
{
    Task LoadAsync(CancellationToken ct);

    string Icon { get; set; }

    string Fio { get; set; }

    string DataProvider { get; set; }

    string ThemeIcon { get; set; }

    int TotalRecordCount { get; set; }

    StatItem[] StatItems { get; set; }

    StatItem[] TreeItems { get; set; }
}