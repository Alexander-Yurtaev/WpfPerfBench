using WpfPerfBench.Interfaces.Managers;

namespace WpfPerfBench.Controls.ContentIndicators;

public partial class StandardContentIndicator(IBusyManager busyManager) 
    : BaseContentIndicator(busyManager);