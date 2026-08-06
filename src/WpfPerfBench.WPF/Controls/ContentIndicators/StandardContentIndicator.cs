using WpfPerfBench.WPF.Interfaces.Managers;

namespace WpfPerfBench.WPF.Controls.ContentIndicators;

public class StandardContentIndicator(IBusyManager busyManager) 
    : BaseContentIndicator(busyManager);