using WpfPerfBench.Data.Enums;

namespace WpfPerfBench.Data;

public interface IUserSession
{
    string Fio { get; set; }
    DataProvider DataProvider { get; set; }
    string? ConnectionString { get; set; }
}