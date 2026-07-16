namespace WpfPerfBench.Data;

public abstract class ResultBase
{
    public bool Success { get; protected set; }
    public string Message { get; protected set; } = string.Empty;

    public static SuccessResult SuccessResult()
    {
        return new SuccessResult();
    }

    public static FailResult FailResult(string message)
    {
        return new FailResult(message);
    }

    public static NamesResult NamesResult(IEnumerable<string> names)
    {
        return new NamesResult(names);
    }
}

public class SuccessResult : ResultBase
{
    public SuccessResult()
    {
        Success = true;
    }
}

public class FailResult : ResultBase
{
    public FailResult(string message)
    {
        Success = false;
        Message = message;
    }
}

public class NamesResult : ResultBase
{
    public IEnumerable<string> Names { get; }

    public NamesResult(IEnumerable<string> names)
    {
        Success = true;
        Names = names;
    }
}