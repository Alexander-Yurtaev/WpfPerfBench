namespace WpfPerfBench.WPF.DesignViewModels;

public class DesignPager
{
    public DesignPager()
    {
        CurrentPageNumber = 2;
        TotalPages = 4;
    }

    public int CurrentPageNumber { get; set; }
    public int TotalPages { get; set; }
}