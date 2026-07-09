using WpfPrefBench.Data.Enums;

namespace WpfPrefBench.Data;

public class UserSession : IUserSession
{
    public DataProvider DataProvider { get; set; }
    public string? ConnectionString { get; set; }
}