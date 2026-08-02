namespace WpfPerfBench.Data;

public abstract class ResultBase
{
    public bool? Success { get; protected set; }
    public string Message { get; protected set; } = string.Empty;

    public static SuccessResult SuccessResult()
    {
        return new SuccessResult();
    }

    public static FailResult FailResult(string message)
    {
        return new FailResult(message);
    }

    public static CancelResult CancelResult(string message = "")
    {
        return new CancelResult(message);
    }

    public static EntityResult<T> EntityResult<T>(IEnumerable<T> entities)
    {
        return new EntityResult<T>(entities);
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

public class CancelResult : ResultBase
{
    public CancelResult(string message = "Операция отменена")
    {
        Success = null;
        Message = message;
    }
}

public class EntityResult<T> : ResultBase
{
    public IEnumerable<T> Entities { get; }

    public EntityResult(IEnumerable<T> entities)
    {
        Success = true;
        Entities = entities;
    }
}