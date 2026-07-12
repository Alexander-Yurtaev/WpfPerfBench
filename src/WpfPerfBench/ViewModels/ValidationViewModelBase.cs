using System.Collections;
using System.ComponentModel;

namespace WpfPerfBench.ViewModels;

public abstract class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
{
    protected readonly Dictionary<string, List<string>> Errors = [];

    #region Implementation of INotifyDataErrorInfo

    public IEnumerable GetErrors(string? propertyName)
    {
        if (string.IsNullOrEmpty(propertyName))
            return Errors.SelectMany(kvp => kvp.Value);

        return Errors.TryGetValue(propertyName, out var error)
            ? error
            : Enumerable.Empty<string>();
    }

    public bool HasErrors => Errors.Any();
    public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

    #endregion

    protected virtual void OnErrorsChanged(string propertyName)
    {
        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        OnPropertyChanged(nameof(HasErrors));
    }
}