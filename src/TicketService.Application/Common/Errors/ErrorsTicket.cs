using TicketService.Domain.Common;

namespace TicketService.Application.Common.ErrorsHandler;

public static class ErrorsTicket
{
    public static readonly Error NotFoundById = new Error("Ticket.NotFoundById", "Can't find ticket in DB with this Id");
}