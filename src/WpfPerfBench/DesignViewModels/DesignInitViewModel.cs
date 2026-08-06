using System.Security;
using WpfPerfBench.Core.Enums;
using WpfPerfBench.Core.Helpers;
using WpfPerfBench.Data;
using WpfPerfBench.Data.Enums;
using WpfPerfBench.Managers;
using WpfPerfBench.ViewModels;

namespace WpfPerfBench.DesignViewModels;

public class DesignInitViewModel : InitViewModel
{
    public DesignInitViewModel() : base(
        new NavigationService(), 
        new UserSession(), 
        null!, 
        new BusyManager(), 
        null!)
    {
        Fio = "Иванов Иван";
        Email = "ivan.ivanov@mail";
        Password = SecurityHelpers.CreateSecureString("A1111111");
        ConfirmPassword = new SecureString();//SecurityHelpers.CreateSecureString("A1111111");
        DbTypes = [DataProvider.Postgres, DataProvider.SqlServer];
        DbType = DbTypes[0];
        ConnectionString = "ConnectionString";

        NavigationService.CurrentPage = Page.Init;
    }
}