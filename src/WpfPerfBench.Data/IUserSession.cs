using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Data;

public interface IUserSession
{
    DataProvider DataProvider { get; set; }
    string? ConnectionString { get; set; }
}