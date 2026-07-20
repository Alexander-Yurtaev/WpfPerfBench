using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Enum;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IInitViewModel : IViewModelBase
{
    void Validate();

    string Fio { get; set; }

    string Email { get; set; }

    string Password { get; set; }

    string ConfirmPassword { get; set; }

    DataProvider[] DbTypes { get; set; }

    DataProvider DbType { get; set; }

    string ConnectionString { get; set; }

    InitState CurrentState { get; set; }

    string ValidationStatus { get; set; }

    IAsyncRelayCommand TestCommand { get; }

    IAsyncRelayCommand MigrateCommand { get; }

    IRelayCommand NextCommand { get; }

    bool HasErrors { get; }
    bool IsValid { get; }

    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}