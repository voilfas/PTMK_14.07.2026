using MediatR;
using TicketService.Application.Abstractions.Cache;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.DTOs.TicketDTOs;

namespace TicketService.Application.Features.Tickets.Queries.GetRecentTickets;

public class GetRecentTicketsHandler : IRequestHandler<GetRecentTicketsQuery, IReadOnlyCollection<TicketListItemDto>>
{
    private readonly ITicketReadRepository _repository;
    private readonly ICacheService _cache;
    
    public GetRecentTicketsHandler(
        ITicketReadRepository repository,
        ICacheService cache)
    {
        _repository = repository;
        _cache =  cache;
    }

    public async Task<IReadOnlyCollection<TicketListItemDto>> Handle
        (GetRecentTicketsQuery request,
            CancellationToken cancellationToken)
    {
        var cacheKey =  "tickets:30";
        
        var cacheTickets = await _cache.GetAsync<IReadOnlyCollection<TicketListItemDto>>(cacheKey);
        
        if (cacheTickets is not null)
            return cacheTickets;
        
        var recentListTickets = await _repository.GetRecentAsync(cancellationToken);
        
        await _cache.SetAsync(cacheKey, recentListTickets, TimeSpan.FromMinutes(5));
        
        return recentListTickets;
    }
}