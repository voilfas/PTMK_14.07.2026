using TicketService.Domain.Common;

namespace TicketService.Domain.Errors;

public static class ErrorsPosition
{
    public static readonly Error EmptyName = new Error("Position.EmptyName", "Name can't be empty");
    public static readonly Error InvalidName = new Error("Position.InvalidName", "Name should contain 2 - 30 symbols");
}