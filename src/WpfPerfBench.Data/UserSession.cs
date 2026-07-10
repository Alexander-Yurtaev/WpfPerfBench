using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Data;

public class UserSession : IUserSession
{
    public DataProvider DataProvider { get; set; }
    public string? ConnectionString { get; set; }
}