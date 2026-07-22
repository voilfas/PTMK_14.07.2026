namespace TicketService.Domain.Common;

public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error? Error { get; }

    protected Result(bool isSuccess, Error? error)
    {
        if (isSuccess && error is not null)
            throw new ArgumentException("Success and Error!");
        
        if (!isSuccess && error is null)
            throw new ArgumentException("Not Success and Not Error!");
        
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new Result(true, null);
    
    public static Result Failure(Error error) => new Result(false, error);
}