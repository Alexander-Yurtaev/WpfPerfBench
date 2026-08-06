using WpfPerfBench.SeedMethods;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignSeedViewModel : SeedViewModel
{
    public DesignSeedViewModel() 
        : base(null!, null!, null!)
    {
    }

    protected override void FillSeedMethods()
    {
        SeedMethods.Add(new ItemsAddRangeMethod(null!, null!, null!, null!));
        SeedMethods.Add(new ParallelItemsAddRangeMethod(null!, null!, null!, null!));
    }
}