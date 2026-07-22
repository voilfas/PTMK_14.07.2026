namespace TicketService.Domain.Common;

public class Error
{
    public string Message { get; private set; }
    public string Code { get; private set; }

    public Error(string code, string message)
    {
        Message = message;
        Code = code;
    }
}