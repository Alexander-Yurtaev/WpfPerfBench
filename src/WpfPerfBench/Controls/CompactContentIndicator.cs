using WpfPerfBench.Managers;

namespace WpfPerfBench.Controls;

public class CompactContentIndicator : BaseContentIndicator
{
    public CompactContentIndicator(IBusyManager busyManager) : base(busyManager)
    {
        IsIndeterminate = true;
    }
}