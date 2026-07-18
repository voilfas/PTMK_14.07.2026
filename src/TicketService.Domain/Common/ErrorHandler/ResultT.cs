namespace TicketService.Domain.Common.ErrorHandler;

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(T value) : base(isSuccess: true, error: null)
    {
        Value = value;
    }

    private Result(Error error): base(isSuccess: false, error)
    {
        Value = default;
    }
    
    public static Result<T> Success(T value) => new Result<T>(value);
    
    public static Result<T> Failure(Error error) => new Result<T>(error);
}