using System.Collections;
using System.ComponentModel;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IValidationViewModelBase : IViewModelBase
{
    public void Validate();
    public IEnumerable GetErrors(string? propertyName);
    public bool HasErrors { get; }
    public bool IsValid { get; }

    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}