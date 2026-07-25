using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTicketById;

public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, Result<TicketDto>>
{
    private readonly ITicketReadRepository _ticketReadRepository;

    public GetTicketByIdHandler(ITicketReadRepository ticketReadRepository)
    {
        _ticketReadRepository = ticketReadRepository;
    }

    public async Task<Result<TicketDto>> Handle(
        GetTicketByIdQuery query,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketReadRepository.GetByIdAsync(query.Id, cancellationToken);
        if (ticket is null)
            return Result<TicketDto>.Failure(ErrorsTicket.NotFoundById);
        
        return Result<TicketDto>.Success(ticket);
    }
}