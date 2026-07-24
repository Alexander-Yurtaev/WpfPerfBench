using System.Collections;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Interfaces.ViewModels;

public interface IInitViewModel : IViewModelBase
{
    #region Form Items

    string Fio { get; set; }

    string Email { get; set; }

    string Password { get; set; }

    string ConfirmPassword { get; set; }

    DataProvider[] DbTypes { get; set; }

    DataProvider DbType { get; set; }

    string ConnectionString { get; set; }

    #endregion Form Items

    //string ValidationStatus { get; set; }

    IAsyncRelayCommand TestCommand { get; }

    //IAsyncRelayCommand MigrateCommand { get; }

    //IRelayCommand NextCommand { get; }

    bool HasErrors { get; }
    bool IsValid { get; }

    void Validate();

    IEnumerable GetErrors(string? propertyName);
    event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
}