using WpfPrefBench.Data.Enums;

namespace WpfPrefBench.Data;

public interface IUserSession
{
    DataProvider DataProvider { get; set; }
    string? ConnectionString { get; set; }
}