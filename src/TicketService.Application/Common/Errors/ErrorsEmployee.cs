using TicketService.Domain.Common;

namespace TicketService.Application.Common.Errors;

public static class ErrorsEmployee
{
    public static readonly Error NotFoundById = new Error("Employee.NotFoundById", "Can't find employee in DB with this Id");
}