using TicketService.Application.Common;
using TicketService.Application.DTOs;
using TicketService.Application.UseCases.Tickets.Queries.GetTickets;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface ITicketReadRepository
{
    Task<TicketDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<PageResult<TicketDto>> GetAllAsync(
        TicketFilter filter,
        CancellationToken cancellationToken = default);
    Task<PageResult<TicketDto>> GetOverdueAsync(
        int page,
        int pageSize,
        CancellationToken cancellationToken = default);
}