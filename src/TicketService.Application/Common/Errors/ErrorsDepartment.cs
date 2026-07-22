using TicketService.Domain.Common;

namespace TicketService.Application.Common.ErrorsHandler;

public static class ErrorsDepartment
{
    public static readonly Error NotFoundById = new Error("Department.NotFoundById", "Can't find department in DB with this Id");
}