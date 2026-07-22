using TicketService.Domain.Common;

namespace TicketService.Application.Common.ErrorsHandler;

public static class ErrorsPosition
{
    public static readonly Error NotFoundById = new  Error("Position.NotFoundById", "Can't find position in DB with this Id"); 
}