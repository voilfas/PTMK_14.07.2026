using TicketService.Application.Common;
using TicketService.Application.Common.Pagination;
using TicketService.Application.DTOs.TicketDTOs;
using TicketService.Application.Features.Tickets.Queries.GetTickets;

namespace TicketService.Application.Abstractions.Persistence.Queries;

public interface ITicketReadRepository
{
    Task<TicketDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    
    Task<PageResult<TicketListItemDto>> GetAllAsync(
        TicketFilter filter,
        CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<TicketStatusReportDto>> GetStatusReportAsync(
        CancellationToken cancellationToken);
    
    Task<int> GetAmountOverdueAsync(
        CancellationToken cancellationToken);
    
    Task<IReadOnlyCollection<TicketCompletedAmountExecutorDto>>  GetCompletedAmountAsync(
        CancellationToken cancellationToken);
}