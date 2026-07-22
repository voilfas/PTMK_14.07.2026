using TicketService.Application.Abstractions.Persistence.Commands;
using TicketService.Application.Common.ErrorsHandler;
using TicketService.Application.DTOs;
using TicketService.Domain.Common;

namespace TicketService.Application.UseCases.Tickets.Queries.GetTicketByIdForUpdate;

public class GetTicketForUpdateHandler
{
    private readonly ITicketRepository _repository;

    public GetTicketForUpdateHandler(ITicketRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<TicketDto>> Handle(
        GetTicketForUpdateQuery request,
        CancellationToken cancellationToken)
    {
        var ticket = await _repository.GetByIdAsync(request.Id, cancellationToken);

        if (ticket is null)
            return Result<TicketDto>.Failure(ErrorsTicket.NotFoundById);
        
        var ticketDto = new TicketDto(
            ticket.Id,
            ticket.TicketNumber.ToString(),
            ticket.Description,
            ticket.Status,
            ticket.Deadline);
        
        return Result<TicketDto>.Success(ticketDto);
    }
}