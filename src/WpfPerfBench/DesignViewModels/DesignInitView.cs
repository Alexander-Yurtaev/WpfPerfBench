using CommunityToolkit.Mvvm.Input;
using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.DesignViewModels;

public class DesignInitView
{
    public DesignInitView()
    {
        Fio = "Иванов Иван";
        Email = "ivan.ivanov@mail";
        Password = "A1234567";
        ConfirmPassword = "A1234567";
        DbTypes = [DataProvider.Postgres, DataProvider.MsSql];
        DbType = DbTypes[0];
        ConnectionString = "ConnectionString";
        TestCommand = new AsyncRelayCommand(async () => await Task.Delay(0), () => true);
    }

    public string Fio { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
    public DataProvider[] DbTypes { get; set; }
    public DataProvider DbType { get; set; }
    public string ConnectionString { get; set; }
    public IAsyncRelayCommand TestCommand { get; set; }
}