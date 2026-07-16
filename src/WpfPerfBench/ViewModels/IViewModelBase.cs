using System.ComponentModel;

namespace WpfPerfBench.ViewModels;

public interface IViewModelBase
{
    public WpfPerfBench.ViewModels.Header Header
    {
        get;
        set;
    }

    public string FooterTitle
    {
        get;
        set;
    }

    event PropertyChangedEventHandler? PropertyChanged;
    event PropertyChangingEventHandler? PropertyChanging;
}