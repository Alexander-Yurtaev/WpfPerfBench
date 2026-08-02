using WpfPerfBench.Enums;

namespace WpfPerfBench.Managers;

public class NavigatableEventArgs
{
    public NavigatableEventArgs(NavigationType type, bool allowed)
    {
        Type = type;
        Allowed = allowed;
    }

    public NavigationType Type { get; }
    public bool Allowed { get; }
}