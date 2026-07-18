using TicketService.Domain.Common.ErrorHandler;

namespace TicketService.Domain.Errors;

public static class ErrorsPosition
{
    public static Error EmptyName = new Error("Position.EmptyName", "Name can't be empty");
    public static Error InvalidName = new Error("Position.InvalidName", "Name should contain 2 - 30 symbols");
}