using MediatR;
using TicketService.Application.Abstractions.Cache;
using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.Errors;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Commands.DeleteTicketById;

public class DeleteTicketByIdHandler : IRequestHandler<DeleteTicketByIdCommand, Result>
{
    private readonly ITicketRepository _repository;
    private readonly ICacheService _cache;

    public DeleteTicketByIdHandler(
        ITicketRepository repository,
        ICacheService cache)
    {
        _repository = repository;
        _cache = cache;
    }
    
    public async Task<Result> Handle(DeleteTicketByIdCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);
        
        if (ticket is null)
            return Result.Failure(ErrorsTicket.NotFoundById);
        
        await _repository.DeleteAsync(ticket, cancellationToken);
        
        await _cache.RemoveAsync("tickets:30");
        
        return Result.Success();
    }
}