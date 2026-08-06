using CommunityToolkit.Mvvm.ComponentModel;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Data.Metrics;
using WpfPerfBench.WPF.Wrappers;

namespace WpfPerfBench.WPF.DesignViewModels;

public class DesignSeedMethodItem : ObservableObject
{
    public string Title { get; set; }
    public string Description { get; set; }
    public SeedStatus Status { get; set; }

    public DesignSeedMethodItem()
    {
        Title = "Design Title";
        Description = "Design Description";
        Status = SeedStatus.None;

        var metrics = new SeedMethodMetrics
        {
            ProcessedItemCount = 0,
            TotalItemCount = 0,
            MemoryBefore = 1_234_567,
            MemoryAfter = 7_654_321,
            Duration = TimeSpan.Parse("17:59:31.6334929")
        };

        MethodMetrics = new SeedMethodMetricsWrapper(metrics);
    }

    public SeedMethodMetricsWrapper MethodMetrics { get; set; }
}