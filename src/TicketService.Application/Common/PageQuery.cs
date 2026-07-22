namespace TicketService.Application.Common;

public abstract record PageQuery(
    int Page = 1,
    int PageSize = 10);