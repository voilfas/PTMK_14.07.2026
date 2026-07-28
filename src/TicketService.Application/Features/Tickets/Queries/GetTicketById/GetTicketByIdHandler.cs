using MediatR;
using TicketService.Application.Abstractions.Persistence.Queries;
using TicketService.Application.Common.Errors;
using TicketService.Application.DTOs.TicketDTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.Features.Tickets.Queries.GetTicketById;

public class GetTicketByIdHandler : IRequestHandler<GetTicketByIdQuery, Result<TicketDto>>
{
    private readonly ITicketReadRepository _ticketReadRepository;

    public GetTicketByIdHandler(ITicketReadRepository ticketReadRepository)
    {
        _ticketReadRepository = ticketReadRepository;
    }

    public async Task<Result<TicketDto>> Handle(
        GetTicketByIdQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _ticketReadRepository.GetByIdAsync(request.Id, cancellationToken);
        if (ticket is null)
            return Result<TicketDto>.Failure(ErrorsTicket.NotFoundById);
        
        return Result<TicketDto>.Success(ticket);
    }
}