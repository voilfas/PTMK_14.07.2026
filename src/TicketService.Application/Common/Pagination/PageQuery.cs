namespace TicketService.Application.Common.Pagination;

public abstract record PageQuery(
    int Page = 1,
    int PageSize = 10);